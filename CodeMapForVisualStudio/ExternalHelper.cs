using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace CodeMapForVisualStudio
{
    public static class ExternalHelper
    {
        private static readonly Brush defaultMaskBrush = new SolidColorBrush(Color.FromArgb(100, 0, 168, 255));
        private static readonly Brush defaultPrivateBrush = new SolidColorBrush(Color.FromArgb(255, 160, 160, 160));
        private static readonly Brush defaultInternalBrush = new SolidColorBrush(Color.FromArgb(255, 172, 172, 172));
        private static readonly Brush defaultProtectedBrush = new SolidColorBrush(Color.FromArgb(255, 184, 184, 184));
        private static readonly Brush defaultPublicBrush = new SolidColorBrush(Color.FromArgb(255, 196, 196, 196));

        private static readonly string defaultFontFamilyName = "Times New Roman";
        private static readonly double defaultFontSize = 15;
        private static FontWeight defaultFontWeight = FontWeights.Normal;
        private static readonly double defaultLeftMargin = 4;
        private static readonly double defaultTopMargin = 1;
        private static readonly double defaultRightMargin = 1;
        private static readonly double defaultBottomMargin = 1;

        public static Brush MaskBrush = defaultMaskBrush;
        public static Brush PrivateBrush = defaultPrivateBrush;
        public static Brush InternalBrush = defaultInternalBrush;
        public static Brush ProtectedBrush = defaultProtectedBrush;
        public static Brush PublicBrush = defaultPublicBrush;

        public static string FontFamilyName = defaultFontFamilyName;
        public static double FontSize = defaultFontSize;
        public static FontWeight FontWeight = defaultFontWeight;
        public static double LeftMargin = defaultLeftMargin;
        public static double TopMargin = defaultTopMargin;
        public static double RightMargin = defaultRightMargin;
        public static double BottomMargin = defaultBottomMargin;

        public static readonly string[] SupportedLanguages = new string[] { "C#" };
        internal static readonly Dictionary<string, int> CodeItemsSequence;

        internal static readonly Collection<string> CodeItemNames = new Collection<string>
        {
            typeof(FieldCodeItem).FullName,
            typeof(EventFieldCodeItem).FullName,
            typeof(DelegateCodeItem).FullName,
            typeof(ConstructorCodeItem).FullName,
            typeof(PropertyCodeItem).FullName,
            typeof(EventCodeItem).FullName,
            typeof(MethodCodeItem).FullName,
            typeof(InterfaceCodeItem).FullName,
            typeof(ClassCodeItem).FullName,
            typeof(StructCodeItem).FullName,
            typeof(EnumCodeItem).FullName,
            typeof(EnumMemberCodeItem).FullName
        };

        internal static readonly string MappingDeclarationSyntaxMethodName = "MappingDeclarationSyntax";

        internal static readonly string privateTag = "private";
        internal static readonly string protectedInternalTag = "protected internal";
        internal static readonly string internalTag = "internal";
        internal static readonly string protectedTag = "protected";
        internal static readonly string publicTag = "public";

        static ExternalHelper()
        {
            CodeItemsSequence = new Dictionary<string, int>();
            for (var i = 0; i < CodeItemNames.Count; i++)
                CodeItemsSequence[CodeItemNames[i]] = i;
        }

        public static void ResetFontStyles()
        {
            FontFamilyName = defaultFontFamilyName;
            FontSize = defaultFontSize;
            FontWeight = defaultFontWeight;
            LeftMargin = defaultLeftMargin;
            TopMargin = defaultTopMargin;
            RightMargin = defaultRightMargin;
            BottomMargin = defaultBottomMargin;
        }

        public static void ResetColors()
        {
            MaskBrush = defaultMaskBrush;
            PrivateBrush = defaultPrivateBrush;
            InternalBrush = defaultInternalBrush;
            ProtectedBrush = defaultProtectedBrush;
            PublicBrush = defaultPublicBrush;
        }

        public static Collection<CodeItem> ParseMemberDeclarationSyntax(IEnumerable<MemberDeclarationSyntax> declarationSyntaxMembers, TextSelection selection)
        {
            if (declarationSyntaxMembers == null || declarationSyntaxMembers.Count() == 0)
                return new Collection<CodeItem>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            var codeItemType = executingAssembly.GetType(typeof(CodeItem).FullName, true, true);
            var types = executingAssembly.GetTypes().Where(t => t.IsSubclassOf(codeItemType));
            var mappedTypes = new Dictionary<string, Type>();
            foreach (var type in types)
            {
                var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                var instance = constructor.Invoke(new object[0]);
                var declarationSyntax = type.GetMethod(MappingDeclarationSyntaxMethodName, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(instance, null).ToString();
                mappedTypes[declarationSyntax] = type;
            }

            var tempMemberCodeItems = new Dictionary<string, Collection<CodeItem>>();
            foreach (var memberDeclarationSyntax in declarationSyntaxMembers)
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

            var memberCodeItems = new Collection<CodeItem>();
            foreach (var items in tempMemberCodeItems.OrderBy(a => CodeItemsSequence[a.Key]))
                foreach (var item in OrderCodeItems(items.Value))
                    memberCodeItems.Add(item);

            tempMemberCodeItems.Clear();
            return memberCodeItems;
        }

        /// <summary>
        /// Get file path from IWpfTextView.
        /// </summary>
        /// <param name="textView">IWpfTextView object.</param>
        /// <returns>file path.</returns>
        internal static string GetPath(this IWpfTextView textView)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            textView.TextBuffer.Properties.TryGetProperty(typeof(IVsTextBuffer), out IVsTextBuffer bufferAdapter);

            if (!(bufferAdapter is IPersistFileFormat persistFileFormat))
                return null;

            persistFileFormat.GetCurFile(out var filePath, out _);
            return filePath;
        }

        internal static Collection<CodeItem> OrderCodeItems(Collection<CodeItem> codeItems)
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
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Public");
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in publics.Where(i => i.Key.Equals(publicTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Public");
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in protecteds.Where(i => !i.Key.Equals(protectedTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Protected");
                    item.Brush = ExternalHelper.ProtectedBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in protecteds.Where(i => i.Key.Equals(protectedTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Protected");
                    item.Brush = ProtectedBrush;
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in protectedInternals.Where(i => !i.Key.Equals(protectedInternalTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = InternalBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in protectedInternals.Where(i => i.Key.Equals(protectedInternalTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = InternalBrush;
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in internals.Where(i => !i.Key.Equals(internalTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = InternalBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in internals.Where(i => i.Key.Equals(internalTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = InternalBrush;
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in privates.Where(i => !i.Key.Equals(privateTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Private");
                    item.Brush = PrivateBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in privates.Where(i => i.Key.Equals(privateTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Private");
                    item.Brush = PrivateBrush;
                    returnCodeItems.Add(item);
                }
            }

            return returnCodeItems;
        }

        private static ImageMoniker GetKnownMonikers(string knownMoniker)
        {
            var monikers = typeof(KnownMonikers).GetProperties();
            var returnMoniker = monikers.FirstOrDefault(m => knownMoniker.Equals(m.Name)).GetValue(null, null);

            return returnMoniker != null ? (ImageMoniker)returnMoniker : KnownMonikers.QuestionMark;
        }
    }
}
