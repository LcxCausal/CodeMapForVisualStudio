using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeMapForVisualStudio
{
    public abstract class CodeItem
    {
        private Location locaion;
        private IEnumerable<string> modifiers;
        private string name;
        private readonly double scaleFactor;

        private CodeItem()
            : this(null)
        { }

        protected CodeItem(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            if (memberDeclarationSyntax == null)
                return;

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
            treeViewItem.Header = ToString();
            treeViewItem.FontSize = 10 * scaleFactor;
            treeViewItem.Foreground = Brushes.WhiteSmoke;

            treeViewItem.MouseEnter += TreeViewItem_MouseEnter;
            treeViewItem.MouseLeave += TreeViewItem_MouseLeave;
            treeViewItem.MouseLeftButtonDown += TreeViewItem_MouseLeftButtonDown;

            return treeViewItem;
        }

        private void TreeViewItem_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            Debug.WriteLine($"Clicked {treeViewItem.Header}");
        }

        private void TreeViewItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            treeViewItem.FontSize = 10 * scaleFactor;
        }

        private void TreeViewItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            treeViewItem.FontSize = 11 * scaleFactor;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", modifiers)} {name}";
        }
    }
}