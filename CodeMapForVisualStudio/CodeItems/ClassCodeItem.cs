using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;

namespace CodeMapForVisualStudio
{
    public class ClassCodeItem : CodeItem
    {
        private const string privateTag = "private";
        private const string internalTag = "internal";
        private const string protectedInternalTag = "protected internal";
        private const string protectedTag = "protected";
        private const string publicTag = "public";

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

            foreach (var field in OrderCodeItems(fields))
                memberCodeItems.Add(field);
            foreach (var constructor in OrderCodeItems(constructors))
                memberCodeItems.Add(constructor);
            foreach (var property in OrderCodeItems(properties))
                memberCodeItems.Add(property);
            foreach (var method in OrderCodeItems(methods))
                memberCodeItems.Add(method);
            foreach (var classItem in OrderCodeItems(classes))
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

            foreach (var codeItem in memberCodeItems)
                treeViewItem.Items.Add(codeItem.ToUIControl());

            return treeViewItem;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Modifiers)} class {Name}";
        }

        private Collection<CodeItem> OrderCodeItems(Collection<CodeItem> codeItems)
        {
            var privates = new Dictionary<string, Collection<CodeItem>>();
            var internals = new Dictionary<string, Collection<CodeItem>>();
            var protectedInternals = new Dictionary<string, Collection<CodeItem>>();
            var protecteds = new Dictionary<string, Collection<CodeItem>>();
            var publics = new Dictionary<string, Collection<CodeItem>>();

            foreach (var codeItem in codeItems.OrderBy(i => i.Name))
            {
                Collection<CodeItem> tempCodeItems = null;
                var modifiers = string.Join(" ", codeItem.Modifiers);

                if (modifiers.StartsWith(privateTag))
                {
                    if (!privates.ContainsKey(modifiers))
                        privates[modifiers] = new Collection<CodeItem>();
                    tempCodeItems = privates[modifiers];
                }
                else if (modifiers.StartsWith(protectedInternalTag))
                {
                    if (!protectedInternals.ContainsKey(modifiers))
                        protectedInternals[modifiers] = new Collection<CodeItem>();
                    tempCodeItems = protectedInternals[modifiers];
                }
                else if (modifiers.StartsWith(protectedTag))
                {
                    if (!protecteds.ContainsKey(modifiers))
                        protecteds[modifiers] = new Collection<CodeItem>();
                    tempCodeItems = protecteds[modifiers];
                }
                else if (modifiers.StartsWith(publicTag))
                {
                    if (!publics.ContainsKey(modifiers))
                        publics[modifiers] = new Collection<CodeItem>();
                    tempCodeItems = publics[modifiers];
                }
                else
                {
                    if (!internals.ContainsKey(modifiers))
                        internals[modifiers] = new Collection<CodeItem>();
                    tempCodeItems = internals[modifiers];
                }

                tempCodeItems.Add(codeItem);
            }

            var returnCodeItems = new Collection<CodeItem>();
            foreach (var items in publics.Where(i => !i.Key.Equals(publicTag)).OrderBy(i => i.Key))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in publics.Where(i => i.Key.Equals(publicTag)))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in protecteds.Where(i => !i.Key.Equals(protectedTag)).OrderBy(i => i.Key))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in protecteds.Where(i => i.Key.Equals(protectedTag)))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in protectedInternals.Where(i => !i.Key.Equals(protectedTag)).OrderBy(i => i.Key))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in protectedInternals.Where(i => i.Key.Equals(protectedInternalTag)))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in internals.Where(i => !i.Key.Equals(internalTag)).OrderBy(i => i.Key))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in internals.Where(i => i.Key.Equals(internalTag)))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in privates.Where(i => !i.Key.Equals(privateTag)).OrderBy(i => i.Key))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);
            foreach (var items in privates.Where(i => i.Key.Equals(privateTag)))
                foreach (var item in items.Value)
                    returnCodeItems.Add(item);

            return returnCodeItems;
        }
    }
}
