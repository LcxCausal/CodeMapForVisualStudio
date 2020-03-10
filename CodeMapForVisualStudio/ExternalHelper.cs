using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CodeMapForVisualStudio
{
    public static class ExternalHelper
    {
        public static readonly Brush maskBrush = new SolidColorBrush(Color.FromArgb(100, 0, 168, 255));
        public static readonly Brush privateBrush = new SolidColorBrush(Color.FromArgb(255, 160, 160, 160));
        public static readonly Brush internalBrush = new SolidColorBrush(Color.FromArgb(255, 172, 172, 172));
        public static readonly Brush protectedBrush = new SolidColorBrush(Color.FromArgb(255, 184, 184, 184));
        public static readonly Brush publicBrush = new SolidColorBrush(Color.FromArgb(255, 196, 196, 196));

        public static string[] SupportedLanguages = new string[] { "C#" };

        public static string fontFamilyName = "Times New Roman";
        public static double fontSize = 15;
        public static FontWeight fontWeight = FontWeights.Normal;
        public static double leftMargin = 4;
        public static double topMargin = 1;
        public static double rightMargin = 1;
        public static double bottomMargin = 1;

        internal static readonly string privateTag = "private";
        internal static readonly string protectedInternalTag = "protected internal";
        internal static readonly string internalTag = "internal";
        internal static readonly string protectedTag = "protected";
        internal static readonly string publicTag = "public";

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
                    item.Brush = ExternalHelper.protectedBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in protecteds.Where(i => i.Key.Equals(protectedTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Protected");
                    item.Brush = protectedBrush;
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in protectedInternals.Where(i => !i.Key.Equals(protectedInternalTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = internalBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in protectedInternals.Where(i => i.Key.Equals(protectedInternalTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = internalBrush;
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in internals.Where(i => !i.Key.Equals(internalTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = internalBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in internals.Where(i => i.Key.Equals(internalTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Internal");
                    item.Brush = internalBrush;
                    returnCodeItems.Add(item);
                }
            }

            foreach (var items in privates.Where(i => !i.Key.Equals(privateTag)).OrderBy(i => i.Key))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Private");
                    item.Brush = privateBrush;
                    returnCodeItems.Add(item);
                }
            }
            foreach (var items in privates.Where(i => i.Key.Equals(privateTag)))
            {
                foreach (var item in items.Value)
                {
                    item.ImageMoniker = GetKnownMonikers($"{item.GetCodeType()}Private");
                    item.Brush = privateBrush;
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
