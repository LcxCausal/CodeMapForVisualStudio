using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace CodeMapForVisualStudio
{
    public class EventCodeItem : CodeItem
    {
        private readonly string accessors;
        private readonly string propertyType;

        private EventCodeItem()
            : this(null, null)
        { }

        public EventCodeItem(EventDeclarationSyntax eventDeclarationSyntax, TextSelection selection)
            : base(eventDeclarationSyntax, selection)
        {
            if (eventDeclarationSyntax == null)
                return;

            accessors = string.Join("; ", eventDeclarationSyntax.AccessorList.Accessors.Select(a => a.Keyword.ValueText));
            propertyType = eventDeclarationSyntax.Type.ToString();
        }

        public override string ToString()
        {
            return $"{Name} {{ {accessors} }}: {propertyType}";
        }

        protected override string GetCodeTypeCore()
        {
            return "Event";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is EventDeclarationSyntax eventDeclarationSyntax ?
                eventDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(EventDeclarationSyntax).FullName;
        }
    }
}
