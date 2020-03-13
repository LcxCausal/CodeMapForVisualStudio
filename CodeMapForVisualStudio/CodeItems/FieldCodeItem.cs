using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeMapForVisualStudio
{
    public class FieldCodeItem : CodeItem
    {
        private readonly string fieldType;

        private FieldCodeItem()
            : this(null, null)
        { }

        public FieldCodeItem(FieldDeclarationSyntax fieldDeclarationSyntax, TextSelection selection)
            : base(fieldDeclarationSyntax, selection)
        {
            if (fieldDeclarationSyntax == null)
                return;

            fieldType = fieldDeclarationSyntax.Declaration.Type.ToString();
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is FieldDeclarationSyntax fieldDeclarationSyntax ?
                fieldDeclarationSyntax.Declaration.Variables.FirstOrDefault().Identifier.ValueText :
                string.Empty;
        }

        public override string ToString()
        {
            return $"{Name}: {fieldType}";
        }

        protected override string GetCodeTypeCore()
        {
            return "Field";
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(FieldDeclarationSyntax).FullName;
        }
    }
}
