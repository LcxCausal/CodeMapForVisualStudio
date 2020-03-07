using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class FieldCodeItem : CodeItem
    {
        private string fieldType;

        private FieldCodeItem()
            : this(null)
        { }

        public FieldCodeItem(FieldDeclarationSyntax fieldDeclarationSyntax)
            : base(fieldDeclarationSyntax)
        {
            if (fieldDeclarationSyntax == null)
                return;

            fieldType = fieldDeclarationSyntax.Declaration.Type.ToString();
        }

        public string FieldType { get => fieldType; set => fieldType = value; }

        public override string GetNameFromDeclarationSyntax(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is FieldDeclarationSyntax fieldDeclarationSyntax ?
                fieldDeclarationSyntax.Declaration.Variables.FirstOrDefault().Identifier.ValueText :
                string.Empty;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Modifiers)} {fieldType} {Name}";
        }
    }
}
