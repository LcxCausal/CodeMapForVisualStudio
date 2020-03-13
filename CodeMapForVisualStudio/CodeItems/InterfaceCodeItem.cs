using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class InterfaceCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        private InterfaceCodeItem()
            : this(null, null)
        { }

        public InterfaceCodeItem(InterfaceDeclarationSyntax interfaceDeclarationSyntax, TextSelection selection)
            : base(interfaceDeclarationSyntax, selection)
        {
            if (interfaceDeclarationSyntax == null)
                return;

            memberCodeItems = interfaceDeclarationSyntax.Members.Count > 0
                ? ExternalHelper.ParseMemberDeclarationSyntax(interfaceDeclarationSyntax.Members, selection)
                : new Collection<CodeItem>();
        }

        public override string ToString()
        {
            return $"{Name}: interface";
        }

        protected override TreeViewItem ToUIControlCore()
        {
            var treeViewItem = base.ToUIControlCore();

            foreach (var codeItem in memberCodeItems)
                treeViewItem.Items.Add(codeItem.ToUIControl());

            return treeViewItem;
        }

        protected override string GetCodeTypeCore()
        {
            return "Interface";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is InterfaceDeclarationSyntax interfaceDeclarationSyntax
                ? interfaceDeclarationSyntax.Identifier.ValueText
                : string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(InterfaceDeclarationSyntax).FullName;
        }
    }
}
