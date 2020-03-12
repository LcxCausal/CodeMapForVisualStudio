using EnvDTE;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Controls;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CodeMapForVisualStudio
{
    public class ClassCodeItem : CodeItem
    {
        private readonly Collection<CodeItem> memberCodeItems;

        private ClassCodeItem()
            : this(null, null)
        { }

        public ClassCodeItem(ClassDeclarationSyntax classDeclarationSyntax, TextSelection selection)
            : base(classDeclarationSyntax, selection)
        {
            if (classDeclarationSyntax == null)
                return;

            var executingAssembly = Assembly.GetExecutingAssembly();
            var codeItemType = executingAssembly.GetType(typeof(CodeItem).FullName, true, true);
            var types = executingAssembly.GetTypes().Where(t => t.IsSubclassOf(codeItemType));
            var mappedTypes = new Dictionary<string, Type>();
            foreach (var type in types)
            {
                var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                var instance = constructor.Invoke(new object[0]);
                var declarationSyntax = type.GetMethod(ExternalHelper.MappingDeclarationSyntaxMethodName, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(instance, null).ToString();
                mappedTypes[declarationSyntax] = type;
            }

            var tempMemberCodeItems = new Dictionary<string, Collection<CodeItem>>();
            foreach (var memberDeclarationSyntax in classDeclarationSyntax.Members)
            {
                var typeName = memberDeclarationSyntax.GetType().FullName;
                if (mappedTypes.ContainsKey(typeName))
                {
                    var mappedCodeItemType = mappedTypes[typeName];
                    if (!tempMemberCodeItems.ContainsKey(mappedCodeItemType.FullName))
                        tempMemberCodeItems[mappedCodeItemType.FullName] = new Collection<CodeItem>();
                    tempMemberCodeItems[mappedCodeItemType.FullName].Add((CodeItem)mappedTypes[typeName].GetConstructors()[0].Invoke(new object[] { memberDeclarationSyntax, selection }));
                }
            }

            memberCodeItems = new Collection<CodeItem>();
            foreach (var items in tempMemberCodeItems.OrderBy(a => ExternalHelper.CodeItemsSequence[a.Key]))
                foreach (var item in ExternalHelper.OrderCodeItems(items.Value))
                    memberCodeItems.Add(item);

            tempMemberCodeItems.Clear();
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

        protected override string MappingDeclarationSyntaxCore()
        {
            return typeof(ClassDeclarationSyntax).FullName;
        }
    }
}
