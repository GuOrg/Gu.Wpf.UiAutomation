namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class SpreadsheetItemPatternProperties : ISpreadsheetItemPatternProperties
    {
        public PropertyId Formula => PropertyId.NotSupportedByFramework;

        public PropertyId AnnotationObjects => PropertyId.NotSupportedByFramework;

        public PropertyId AnnotationTypes => PropertyId.NotSupportedByFramework;
    }
}