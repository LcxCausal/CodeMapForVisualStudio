using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeMapForVisualStudio
{
    public abstract class CodeItem
    {
        private readonly double scaleFactor;
        private readonly TextSelection selection;

        private Location locaion;
        private IEnumerable<string> modifiers;
        private string name;

        private CodeItem()
            : this(null, null)
        { }

        protected CodeItem(MemberDeclarationSyntax memberDeclarationSyntax, TextSelection selection)
        {
            if (memberDeclarationSyntax == null)
                return;

            this.selection = selection;
            scaleFactor = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice.M11;
            Location = memberDeclarationSyntax.GetLocation();
            Modifiers = memberDeclarationSyntax.Modifiers.Select(m => m.ValueText);
            Name = GetNameFromDeclarationSyntax(memberDeclarationSyntax);
        }

        public Location Location { get => locaion; protected set => locaion = value; }

        public IEnumerable<string> Modifiers { get => modifiers; protected set => modifiers = value; }

        public string Name { get => name; protected set => name = value; }

        public abstract string GetNameFromDeclarationSyntax(MemberDeclarationSyntax memberDeclarationSyntax);

        public virtual TreeViewItem ToUIControl()
        {
            var treeViewItem = new TreeViewItem();
            treeViewItem.IsExpanded = true;
            treeViewItem.Header = ToString();
            treeViewItem.FontFamily = new FontFamily("Times New Roman");
            treeViewItem.FontSize = 10 * scaleFactor;
            treeViewItem.Foreground = new SolidColorBrush(Color.FromArgb(255, 196, 196, 196));

            treeViewItem.MouseEnter += TreeViewItem_MouseEnter;
            treeViewItem.MouseLeave += TreeViewItem_MouseLeave;
            treeViewItem.MouseLeftButtonUp += TreeViewItem_MouseLeftButtonUp;

            return treeViewItem;
        }

        private void TreeViewItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!((TreeViewItem)sender).IsSelected)
                return;

            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            var startLinePosition = locaion.GetLineSpan().StartLinePosition;
            selection.MoveToLineAndOffset(startLinePosition.Line + 1, startLinePosition.Character + 1);
        }

        private void TreeViewItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            treeViewItem.FontSize = 10 * scaleFactor;
        }

        private void TreeViewItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            treeViewItem.FontSize = 10.5 * scaleFactor;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", modifiers)} {name}";
        }
    }
}