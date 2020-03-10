using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeMapForVisualStudio
{
    public abstract class CodeItem
    {
        private readonly TextSelection selection;

        private Location location;
        private IEnumerable<string> modifiers;
        private string name;

        private Brush brush;
        private ImageMoniker imageMoniker;

        private CodeItem()
            : this(null, null)
        { }

        protected CodeItem(MemberDeclarationSyntax memberDeclarationSyntax, TextSelection selection)
        {
            if (memberDeclarationSyntax == null)
                return;

            this.selection = selection;
            this.brush = ExternalHelper.publicBrush;
            this.imageMoniker = KnownMonikers.QuestionMark;

            this.location = memberDeclarationSyntax.GetLocation();
            this.modifiers = memberDeclarationSyntax.Modifiers.Select(m => m.ValueText);
            this.name = GetNameFromDeclarationSyntaxCore(memberDeclarationSyntax);
        }

        public Location Location { get => location; protected set => location = value; }

        public IEnumerable<string> Modifiers { get => modifiers; protected set => modifiers = value; }

        public string Name { get => name; protected set => name = value; }

        internal Brush Brush { get => brush; set => brush = value; }

        internal ImageMoniker ImageMoniker { get => imageMoniker; set => imageMoniker = value; }

        public override string ToString()
        {
            return $"{string.Join(" ", modifiers)} {name}";
        }

        public string GetNameFromDeclarationSyntax(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return GetNameFromDeclarationSyntaxCore(memberDeclarationSyntax);
        }

        protected abstract string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax);

        public TreeViewItem ToUIControl()
        {
            return ToUIControlCore();
        }

        protected virtual TreeViewItem ToUIControlCore()
        {
            var treeViewItem = new TreeViewItem();
            treeViewItem.HeaderTemplate = CreateDataTemplate();
            treeViewItem.Tag = location.SourceSpan;
            treeViewItem.IsExpanded = true;

            treeViewItem.MouseEnter += TreeViewItem_MouseEnter;
            treeViewItem.MouseLeave += TreeViewItem_MouseLeave;
            treeViewItem.MouseLeftButtonUp += TreeViewItem_MouseLeftButtonUp;

            return treeViewItem;
        }

        internal DataTemplate CreateDataTemplate()
        {
            return CreateDataTemplateCore();
        }

        protected virtual DataTemplate CreateDataTemplateCore()
        {
            var stackPanel = new FrameworkElementFactory(typeof(StackPanel));
            stackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            var imageMoniker = new FrameworkElementFactory(typeof(CrispImage));
            imageMoniker.SetValue(CrispImage.MonikerProperty, ImageMoniker);
            stackPanel.AppendChild(imageMoniker);

            var text = new FrameworkElementFactory(typeof(TextBlock));
            text.SetValue(TextBlock.TextProperty, ToString());
            text.SetValue(TextBlock.FontFamilyProperty, new FontFamily(ExternalHelper.fontFamilyName));
            text.SetValue(TextBlock.FontSizeProperty, ExternalHelper.fontSize);
            text.SetValue(TextBlock.ForegroundProperty, brush);
            text.SetValue(TextBlock.FontWeightProperty, ExternalHelper.fontWeight);
            text.SetValue(FrameworkElement.MarginProperty, new Thickness(ExternalHelper.leftMargin, ExternalHelper.topMargin, ExternalHelper.rightMargin, ExternalHelper.bottomMargin));
            stackPanel.AppendChild(text);

            var dataTemplate = new DataTemplate();
            dataTemplate.VisualTree = stackPanel;

            return dataTemplate;
        }

        internal string GetCodeType()
        {
            return GetCodeTypeCore();
        }

        protected abstract string GetCodeTypeCore();

        private void TreeViewItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!((TreeViewItem)sender).IsSelected)
                return;

            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            var startLinePosition = location.GetLineSpan().StartLinePosition;
            selection.MoveToLineAndOffset(startLinePosition.Line + 1, startLinePosition.Character + 1);
        }

        private void TreeViewItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            treeViewItem.Background = Brushes.Transparent;
        }

        private void TreeViewItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;

            if (!treeViewItem.HasItems)
                treeViewItem.Background = ExternalHelper.maskBrush;
        }
    }
}