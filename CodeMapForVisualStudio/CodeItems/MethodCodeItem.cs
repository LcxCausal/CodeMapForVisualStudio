using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;

namespace CodeMapForVisualStudio
{
    public class MethodCodeItem : CodeItem
    {
        private readonly string returnType;
        private readonly string parameters;

        private MethodCodeItem()
            : this(null, null)
        { }

        public MethodCodeItem(MethodDeclarationSyntax methodDeclarationSyntax, TextSelection selection)
            : base(methodDeclarationSyntax, selection)
        {
            if (methodDeclarationSyntax == null)
                return;

            returnType = methodDeclarationSyntax.ReturnType.ToString();

            var parametersBuilder = new Collection<string>();
            foreach (var parameter in methodDeclarationSyntax.ParameterList.Parameters)
                parametersBuilder.Add($"{parameter.Type.ToString()} {parameter.Identifier.ValueText}");
            parameters = string.Join(", ", parametersBuilder);
        }

        public string ReturnType { get => returnType; }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is MethodDeclarationSyntax methodDeclarationSyntax ?
                methodDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        public override string ToString()
        {
            return $"{Name}({parameters}): {returnType}";
        }

        protected override string GetCodeTypeCore()
        {
            return "Method";
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(MethodDeclarationSyntax).FullName;
        }
    }
}
