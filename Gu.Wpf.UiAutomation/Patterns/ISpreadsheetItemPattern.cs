namespace Gu.Wpf.UiAutomation
{
    public interface ISpreadsheetItemPattern : IPattern
    {
        ISpreadsheetItemPatternProperties Properties { get; }

        AutomationProperty<string> Formula { get; }

        AutomationProperty<AutomationElement[]> AnnotationObjects { get; }

        AutomationProperty<AnnotationType[]> AnnotationTypes { get; }
    }
}