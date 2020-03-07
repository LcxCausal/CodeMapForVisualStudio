using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class MethodCodeItem : CodeItem
    {
        private readonly string returnType;

        public MethodCodeItem(MethodDeclarationSyntax methodDeclarationSyntax)
            : base(methodDeclarationSyntax)
        {
            if (methodDeclarationSyntax == null)
                return;

            returnType = methodDeclarationSyntax.ReturnType.ToString();
        }

        public string ReturnType { get => returnType; }

        public override string GetNameFromDeclarationSyntax(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is MethodDeclarationSyntax methodDeclarationSyntax ?
                methodDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Modifiers)} {returnType} {Name}";
        }
    }
}
