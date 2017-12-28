namespace Gu.Wpf.UiAutomation.UIA3.Converters
{
    using System;
    using System.Linq;

    public static class AnnotationTypeConverter
    {
        public static object ToAnnotationType(object nativeAnnotationType)
        {
            switch ((int)nativeAnnotationType)
            {
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Comment:
                    return AnnotationType.Comment;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Footer:
                    return AnnotationType.Footer;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_FormulaError:
                    return AnnotationType.FormulaError;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_GrammarError:
                    return AnnotationType.GrammarError;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Header:
                    return AnnotationType.Header;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Highlighted:
                    return AnnotationType.Highlighted;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_SpellingError:
                    return AnnotationType.SpellingError;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_TrackChanges:
                    return AnnotationType.TrackChanges;
                case Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Unknown:
                    return AnnotationType.Unknown;
                default:
                    throw new NotSupportedException();
            }
        }

        public static object ToAnnotationTypeNative(AnnotationType annotationType)
        {
            switch (annotationType)
            {
                case AnnotationType.Comment:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Comment;
                case AnnotationType.Footer:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Footer;
                case AnnotationType.FormulaError:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_FormulaError;
                case AnnotationType.GrammarError:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_GrammarError;
                case AnnotationType.Header:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Header;
                case AnnotationType.Highlighted:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Highlighted;
                case AnnotationType.SpellingError:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_SpellingError;
                case AnnotationType.TrackChanges:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_TrackChanges;
                case AnnotationType.Unknown:
                    return Interop.UIAutomationClient.UIA_AnnotationTypes.AnnotationType_Unknown;
                default:
                    throw new NotSupportedException();
            }
        }

        public static object ToAnnotationTypeArray(object nativeAnnotationTypes)
        {
            var origValue = (int[])nativeAnnotationTypes;
            return origValue.Select(x => ToAnnotationType(x)).ToArray();
        }
    }
}
