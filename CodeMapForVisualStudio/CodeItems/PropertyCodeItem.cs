using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class PropertyCodeItem : CodeItem
    {
        private readonly string accessors;
        private readonly string propertyType;

        public PropertyCodeItem(PropertyDeclarationSyntax propertyDeclarationSyntax)
            : base(propertyDeclarationSyntax)
        {
            if (propertyDeclarationSyntax == null)
                return;

            accessors = string.Join(" ", propertyDeclarationSyntax.AccessorList.Accessors.Select(a => a.Keyword.ValueText));
            propertyType = propertyDeclarationSyntax.Type.ToString();
        }

        public string Accessors { get => accessors; }

        public string PropertyType { get => propertyType; }

        public override string GetNameFromDeclarationSyntax(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is PropertyDeclarationSyntax propertyDeclarationSyntax ?
                propertyDeclarationSyntax.Identifier.ValueText :
                string.Empty;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Modifiers)} {propertyType} {Name}: {accessors}";
        }
    }
}
