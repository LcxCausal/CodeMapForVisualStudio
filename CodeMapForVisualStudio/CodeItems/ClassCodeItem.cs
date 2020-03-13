using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class ClassCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        private ClassCodeItem()
            : this(null, null)
        { }

        public ClassCodeItem(ClassDeclarationSyntax classDeclarationSyntax, TextSelection selection)
            : base(classDeclarationSyntax, selection)
        {
            if (classDeclarationSyntax == null)
                return;

            memberCodeItems = classDeclarationSyntax.Members.Count > 0
                ? ExternalHelper.ParseMemberDeclarationSyntax(classDeclarationSyntax.Members, selection)
                : new Collection<CodeItem>();
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is ClassDeclarationSyntax classDeclarationSyntax
                ? classDeclarationSyntax.Identifier.ValueText
                : string.Empty;
        }

        protected override TreeViewItem ToUIControlCore()
        {
            var treeViewItem = base.ToUIControlCore();

            foreach (var codeItem in memberCodeItems)
                treeViewItem.Items.Add(codeItem.ToUIControl());

            return treeViewItem;
        }

        public override string ToString()
        {
            return $"{Name}: class";
        }

        protected override string GetCodeTypeCore()
        {
            return "Class";
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(ClassDeclarationSyntax).FullName;
        }
    }
}
