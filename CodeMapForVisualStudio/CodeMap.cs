using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Editor;
using System.Diagnostics;

namespace CodeMapForVisualStudio
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("d910937f-89b5-4633-b592-040b1ca35401")]
    [ProvideToolWindow(typeof(CodeMap), Style = VsDockStyle.Float)]
    public class CodeMap : ToolWindowPane
    {
        private readonly ScrollViewer codeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeMap"/> class.
        /// </summary>
        public CodeMap() : base(null)
        {
            Caption = "CodeMap";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new CodeMapControl();
            codeMap = ((CodeMapControl)Content).codeMap;
            codeMap.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            codeMap.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            codeMap.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            codeMap.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            codeMap.PreviewMouseWheel += CodeMap_PreviewMouseWheel;
        }

        private void CodeMap_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (codeMap == null)
                return;

            double number = Math.Abs(e.Delta / 2);
            var offset = e.Delta > 0
                ? Math.Max(0.0, codeMap.VerticalOffset - number)
                : Math.Min(codeMap.ScrollableHeight, codeMap.VerticalOffset + number);

            if (offset != codeMap.VerticalOffset)
            {
                codeMap.ScrollToVerticalOffset(offset);
                e.Handled = true;
            }
        }

        public override void OnToolWindowCreated()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            base.OnToolWindowCreated();

            var codeMapPackage = Package as CodeMapPackage;

            // Bind window activeted and closing event.
            var windowEvents = codeMapPackage.DTE.Events.WindowEvents;
            windowEvents.WindowActivated += WindowEvents_WindowActivated;
            windowEvents.WindowClosing += WindowEvents_WindowClosing;

            // Bind document saved, opened and closing events.
            var documentEvents = codeMapPackage.DTE.Events.DocumentEvents;
            documentEvents.DocumentSaved += DocumentEvents_DocumentSaved;
            documentEvents.DocumentOpened += DocumentEvents_DocumentOpened;
        }

        private void WindowEvents_WindowClosing(Window Window)
        {
            codeMap.Content = null;
        }

        private void WindowEvents_WindowActivated(Window GotFocus, Window LostFocus)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            UpdateCodeMap(GotFocus.Document);

            var viewHost = GetCurrentViewHost();
            if (viewHost != null)
                viewHost.TextView.Caret.PositionChanged += Caret_PositionChanged;
        }

        private void Caret_PositionChanged(object sender, CaretPositionChangedEventArgs e)
        {
            var position = e.NewPosition.BufferPosition.Position;
            var matchedTreeViewItem = MatchTreeViewItem(position);

            ClearCodeMapMask();

            if (matchedTreeViewItem.TreeViewItem != null)
            {
                matchedTreeViewItem.TreeViewItem.Background = ExternalHelper.MaskBrush;
                if (matchedTreeViewItem.VerticalOffset != 0)
                    codeMap.ScrollToVerticalOffset(matchedTreeViewItem.VerticalOffset);
            }
        }

        private void DocumentEvents_DocumentOpened(Document document)
        {
            UpdateCodeMap(document);
        }

        private void DocumentEvents_DocumentSaved(Document document)
        {
            UpdateCodeMap(document);
        }

        private async void UpdateCodeMap(Document document)
        {
            if (document == null)
                return;

            var codeItems = await MapDocumentAsync(document);

            if (codeItems == null)
                return;

            var codeTree = new TreeView() { Background = Brushes.Transparent };
            codeMap.Content = codeTree;

            foreach (var codeItem in codeItems)
                codeTree.Items.Add(codeItem.ToUIControl());
        }

        private static async Task<Collection<CodeItem>> MapDocumentAsync(Document document)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                var filePath = GetFullName(document);
                var currentText = File.ReadAllText(filePath, Encoding.UTF8);
                var csTree = CSharpSyntaxTree.ParseText(currentText);
                var syntaxRoot = await csTree.GetRootAsync();

                if (!ExternalHelper.SupportedLanguages.Contains(syntaxRoot.Language))
                    return null;

                var declarationSyntaxNodes = syntaxRoot.DescendantNodes().OfType<MemberDeclarationSyntax>().Where(n => n.Parent is NamespaceDeclarationSyntax);
                return ExternalHelper.ParseMemberDeclarationSyntax(declarationSyntaxNodes, document.Selection as TextSelection);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new Collection<CodeItem>();
            }
        }

        private static string GetFullName(Document document)
        {
            var name = string.Empty;

            if (document == null) return name;

            try
            {
                System.Windows.Threading.Dispatcher.CurrentDispatcher.VerifyAccess();
                name = document.FullName;
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting FullName for document.", e);
            }
            return name;
        }

        private IWpfTextViewHost GetCurrentViewHost()
        {
            var textManager = (IVsTextManager)GetService(typeof(SVsTextManager));
            textManager.GetActiveView(1, null, out var textView);

            if (!(textView is IVsUserData userData))
                return null;
            else
            {
                var guidViewHost = DefGuidList.guidIWpfTextViewHost;
                userData.GetData(ref guidViewHost, out var holder);
                return (IWpfTextViewHost)holder;
            }
        }

        private (TreeViewItem TreeViewItem, double VerticalOffset) MatchTreeViewItem(int position)
        {
            if (codeMap.Content == null)
                return (null, 0);

            var treeViewItems = ((TreeView)codeMap.Content).Items;
            var returnTreeViewItemIndex = 0;
            var returnTreeViewItem = treeViewItems == null || treeViewItems.Count == 0 ? null : MatchTreeViewItemCore(position, treeViewItems, ref returnTreeViewItemIndex);

            return (returnTreeViewItem, returnTreeViewItemIndex * codeMap.ScrollableHeight / GetTreeViewItemsCount(treeViewItems));
        }

        private int GetTreeViewItemsCount(ItemCollection treeViewItems)
        {
            var count = 0;

            foreach (TreeViewItem treeViewItem in treeViewItems)
            {
                count++;
                if (treeViewItem.HasItems)
                    count += GetTreeViewItemsCount(treeViewItem.Items);
            }

            return count;
        }

        private TreeViewItem MatchTreeViewItemCore(int position, ItemCollection treeViewItems, ref int index)
        {
            foreach (TreeViewItem treeViewItem in treeViewItems)
            {
                index++;

                if (!treeViewItem.HasItems)
                {
                    var sourceSpan = (Microsoft.CodeAnalysis.Text.TextSpan)treeViewItem.Tag;
                    if (position >= sourceSpan.Start && position <= sourceSpan.End)
                        return treeViewItem;
                }
                else
                {
                    var returnTreeViewItem = MatchTreeViewItemCore(position, treeViewItem.Items, ref index);
                    if (returnTreeViewItem != null)
                        return returnTreeViewItem;
                }
            }

            return null;
        }

        private void ClearCodeMapMask()
        {
            if (codeMap.Content == null)
                return;

            var treeViewItems = ((TreeView)codeMap.Content).Items;
            if (treeViewItems == null || treeViewItems.Count == 0)
                return;

            ClearCodeMapMaskCore(treeViewItems);
        }

        private void ClearCodeMapMaskCore(ItemCollection treeViewItems)
        {
            foreach (TreeViewItem treeViewItem in treeViewItems)
            {
                treeViewItem.Background = Brushes.Transparent;
                if (treeViewItem.HasItems)
                    ClearCodeMapMaskCore(treeViewItem.Items);
            }
        }
    }
}
