using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeMapForVisualStudio
{
    public class EnumMemberCodeItem : CodeItem
    {
        private readonly string equalsValue;

        private EnumMemberCodeItem()
            : this(null, null)
        { }

        public EnumMemberCodeItem(EnumMemberDeclarationSyntax enumMemberDeclarationSyntax, TextSelection selection)
            : base(enumMemberDeclarationSyntax, selection)
        {
            if (enumMemberDeclarationSyntax == null)
                return;

            equalsValue = enumMemberDeclarationSyntax.EqualsValue?.Value.ToString();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(equalsValue) ? $"{Name}" : $"{Name} = {equalsValue}";
        }

        protected override string GetCodeTypeCore()
        {
            return "EnumerationItem";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is EnumMemberDeclarationSyntax enumMemberDeclarationSyntax ?
                enumMemberDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(EnumMemberDeclarationSyntax).FullName;
        }
    }
}
