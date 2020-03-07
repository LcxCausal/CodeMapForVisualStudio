using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class ClassCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        public ClassCodeItem(ClassDeclarationSyntax classDeclarationSyntax)
            : base(classDeclarationSyntax)
        {
            var fields = new Collection<CodeItem>();
            var properties = new Collection<CodeItem>();
            var methods = new Collection<CodeItem>();
            var classes = new Collection<CodeItem>();

            foreach (var memberDeclarationSyntax in classDeclarationSyntax.Members)
            {
                if (memberDeclarationSyntax is FieldDeclarationSyntax fieldDeclarationSyntax)
                    fields.Add(new FieldCodeItem(fieldDeclarationSyntax));
                else if (memberDeclarationSyntax is PropertyDeclarationSyntax propertyDeclarationSyntax)
                    properties.Add(new PropertyCodeItem(propertyDeclarationSyntax));
                else if (memberDeclarationSyntax is MethodDeclarationSyntax methodDeclarationSyntax)
                    methods.Add(new MethodCodeItem(methodDeclarationSyntax));
                else if (memberDeclarationSyntax is ClassDeclarationSyntax subClassDeclarationSyntax)
                    classes.Add(new ClassCodeItem(subClassDeclarationSyntax));
            }

            memberCodeItems = new Collection<CodeItem>();

            foreach (var field in fields)
                memberCodeItems.Add(field);
            foreach (var property in properties)
                memberCodeItems.Add(property);
            foreach (var method in methods)
                memberCodeItems.Add(method);
            foreach (var classItem in classes)
                memberCodeItems.Add(classItem);

            fields.Clear();
            properties.Clear();
            methods.Clear();
            classes.Clear();
        }

        public Collection<CodeItem> MemberCodeItems { get => memberCodeItems; }

        public override string GetNameFromDeclarationSyntax(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is ClassDeclarationSyntax classDeclarationSyntax
                ? classDeclarationSyntax.Identifier.ValueText
                : string.Empty;
        }

        public override TreeViewItem ToUIControl()
        {
            var treeViewItem = base.ToUIControl();

            foreach (var codeItem in memberCodeItems)
                treeViewItem.Items.Add(codeItem.ToUIControl());

            return treeViewItem;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Modifiers)} class {Name}";
        }
    }
}
