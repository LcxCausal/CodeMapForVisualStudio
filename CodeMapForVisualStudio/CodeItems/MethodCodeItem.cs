using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeMapForVisualStudio
{
    public class MethodCodeItem : CodeItem
    {
        private readonly string returnType;

        private MethodCodeItem()
            : this(null, null)
        { }

        public MethodCodeItem(MethodDeclarationSyntax methodDeclarationSyntax, TextSelection selection)
            : base(methodDeclarationSyntax, selection)
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
