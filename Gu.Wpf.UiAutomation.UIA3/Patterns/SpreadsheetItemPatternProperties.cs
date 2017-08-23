namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class SpreadsheetItemPatternProperties : ISpreadsheetItemPatternProperties
    {
        public PropertyId Formula => SpreadsheetItemPattern.FormulaProperty;

        public PropertyId AnnotationObjects => SpreadsheetItemPattern.AnnotationObjectsProperty;

        public PropertyId AnnotationTypes => SpreadsheetItemPattern.AnnotationTypesProperty;
    }
}