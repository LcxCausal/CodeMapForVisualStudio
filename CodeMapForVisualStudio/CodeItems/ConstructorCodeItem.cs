using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;

namespace CodeMapForVisualStudio
{
    public class ConstructorCodeItem : CodeItem
    {
        private readonly string parameters;

        private ConstructorCodeItem()
            : this(null, null)
        { }

        public ConstructorCodeItem(ConstructorDeclarationSyntax constructorDeclarationSyntax, TextSelection selection) : base(constructorDeclarationSyntax, selection)
        {
            var parametersBuilder = new Collection<string>();
            foreach (var parameter in constructorDeclarationSyntax.ParameterList.Parameters)
                parametersBuilder.Add($"{parameter.Type.ToString()} {parameter.Identifier.ValueText}");
            parameters = string.Join(" ", parametersBuilder);
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is ConstructorDeclarationSyntax constructorDeclarationSyntax ?
                constructorDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        public override string ToString()
        {
            return $"{Name}({parameters})";
        }

        protected override string GetCodeTypeCore()
        {
            return "Method";
        }
    }
}
