using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class StructCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        private StructCodeItem()
            : this(null, null)
        { }

        public StructCodeItem(StructDeclarationSyntax structDeclarationSyntax, TextSelection selection)
            : base(structDeclarationSyntax, selection)
        {
            if (structDeclarationSyntax == null)
                return;

            memberCodeItems = structDeclarationSyntax.Members.Count > 0
                ? ExternalHelper.ParseMemberDeclarationSyntax(structDeclarationSyntax.Members, selection)
                : new Collection<CodeItem>();
        }

        public override string ToString()
        {
            return $"{Name}: struct";
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
            return "Structure";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is StructDeclarationSyntax structDeclarationSyntax
                ? structDeclarationSyntax.Identifier.ValueText
                : string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(StructDeclarationSyntax).FullName;
        }
    }
}
