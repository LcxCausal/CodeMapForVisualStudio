using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class EnumCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        private EnumCodeItem()
            : this(null, null)
        { }

        public EnumCodeItem(EnumDeclarationSyntax enumDeclarationSyntax, TextSelection selection) : base(enumDeclarationSyntax, selection)
        {
            if (enumDeclarationSyntax == null)
                return;

            memberCodeItems = enumDeclarationSyntax.Members.Count > 0
                ? ExternalHelper.ParseMemberDeclarationSyntax(enumDeclarationSyntax.Members, selection)
                : new Collection<CodeItem>();
        }

        public override string ToString()
        {
            return $"{Name}: enum";
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
            return "Enumeration";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is EnumDeclarationSyntax enumDeclarationSyntax
                ? enumDeclarationSyntax.Identifier.ValueText
                : string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(EnumDeclarationSyntax).FullName;
        }
    }
}
