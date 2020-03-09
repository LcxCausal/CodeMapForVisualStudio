using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CodeMapForVisualStudio
{
    public class ClassCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        public ClassCodeItem(ClassDeclarationSyntax classDeclarationSyntax, TextSelection selection)
            : base(classDeclarationSyntax, selection)
        {
            var fields = new Collection<CodeItem>();
            var constructors = new Collection<CodeItem>();
            var properties = new Collection<CodeItem>();
            var methods = new Collection<CodeItem>();
            var classes = new Collection<CodeItem>();

            foreach (var memberDeclarationSyntax in classDeclarationSyntax.Members)
            {
                if (memberDeclarationSyntax is FieldDeclarationSyntax fieldDeclarationSyntax)
                    fields.Add(new FieldCodeItem(fieldDeclarationSyntax, selection));
                else if (memberDeclarationSyntax is ConstructorDeclarationSyntax constructorDeclarationSyntax)
                    constructors.Add(new ConstructorCodeItem(constructorDeclarationSyntax, selection));
                else if (memberDeclarationSyntax is PropertyDeclarationSyntax propertyDeclarationSyntax)
                    properties.Add(new PropertyCodeItem(propertyDeclarationSyntax, selection));
                else if (memberDeclarationSyntax is MethodDeclarationSyntax methodDeclarationSyntax)
                    methods.Add(new MethodCodeItem(methodDeclarationSyntax, selection));
                else if (memberDeclarationSyntax is ClassDeclarationSyntax subClassDeclarationSyntax)
                    classes.Add(new ClassCodeItem(subClassDeclarationSyntax, selection));
            }

            memberCodeItems = new Collection<CodeItem>();

            foreach (var field in ExternalHelper.OrderCodeItems(fields))
                memberCodeItems.Add(field);
            foreach (var constructor in ExternalHelper.OrderCodeItems(constructors))
                memberCodeItems.Add(constructor);
            foreach (var property in ExternalHelper.OrderCodeItems(properties))
                memberCodeItems.Add(property);
            foreach (var method in ExternalHelper.OrderCodeItems(methods))
                memberCodeItems.Add(method);
            foreach (var classItem in ExternalHelper.OrderCodeItems(classes))
                memberCodeItems.Add(classItem);

            fields.Clear();
            properties.Clear();
            methods.Clear();
            classes.Clear();
        }

        public Collection<CodeItem> MemberCodeItems { get => memberCodeItems; }

        protected override string GetNameFromDeclarationSyntaxCore(MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax != null && memberDeclarationSyntax is ClassDeclarationSyntax classDeclarationSyntax
                ? classDeclarationSyntax.Identifier.ValueText
                : string.Empty;
        }

        protected override TreeViewItem ToUIControlCore()
        {
            var treeViewItem = base.ToUIControlCore();
            treeViewItem.FontWeight = System.Windows.FontWeights.Bold;

            foreach (var codeItem in memberCodeItems)
                treeViewItem.Items.Add(codeItem.ToUIControl());

            return treeViewItem;
        }

        public override string ToString()
        {
            return $"{Name}: class";
        }

        protected override string GetCodeTypeCore()
        {
            return "Class";
        }
    }
}
