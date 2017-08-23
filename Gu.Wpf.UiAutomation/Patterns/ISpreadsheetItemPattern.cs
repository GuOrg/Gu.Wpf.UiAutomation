namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISpreadsheetItemPattern : IPattern
    {
        ISpreadsheetItemPatternProperties Properties { get; }

        AutomationProperty<string> Formula { get; }

        AutomationProperty<AutomationElement[]> AnnotationObjects { get; }

        AutomationProperty<AnnotationType[]> AnnotationTypes { get; }
    }
}