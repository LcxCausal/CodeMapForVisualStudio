using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeMapForVisualStudio
{
    public class EventFieldCodeItem : CodeItem
    {
        private readonly string fieldType;

        private EventFieldCodeItem()
            : this(null, null)
        { }

        public EventFieldCodeItem(EventFieldDeclarationSyntax eventFieldDeclarationSyntax, TextSelection selection)
            : base(eventFieldDeclarationSyntax, selection)
        {
            if (eventFieldDeclarationSyntax == null)
                return;

            fieldType = eventFieldDeclarationSyntax.Declaration.Type.ToString();
        }

        public override string ToString()
        {
            return $"{Name}: {fieldType}";
        }

        protected override string GetCodeTypeCore()
        {
            return "Event";
        }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is EventFieldDeclarationSyntax eventFieldDeclarationSyntax ?
                eventFieldDeclarationSyntax.Declaration.Variables.FirstOrDefault().Identifier.ValueText :
                string.Empty;
        }

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(EventFieldDeclarationSyntax).FullName;
        }
    }
}
