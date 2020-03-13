using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;

namespace CodeMapForVisualStudio
{
    public class DelegateCodeItem : CodeItem
    {
        private readonly string returnType;
        private readonly string parameters;

        private DelegateCodeItem()
            : this(null, null)
        { }

        public DelegateCodeItem(DelegateDeclarationSyntax delegateDeclarationSyntax, TextSelection selection)
            : base(delegateDeclarationSyntax, selection)
        {
            if (delegateDeclarationSyntax == null)
                return;

            returnType = delegateDeclarationSyntax.ReturnType.ToString();

            var parametersBuilder = new Collection<string>();
            foreach (var parameter in delegateDeclarationSyntax.ParameterList.Parameters)
                parametersBuilder.Add($"{parameter.Type.ToString()} {parameter.Identifier.ValueText}");
            parameters = string.Join(", ", parametersBuilder);
        }

        public override string ToString()
        {
            return $"{Name}({parameters}): {returnType}";
        }

        protected override string GetCodeTypeCore()
        {
            return "Delegate";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is DelegateDeclarationSyntax delegateDeclarationSyntax ?
                delegateDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(DelegateDeclarationSyntax).FullName;
        }
    }
}
