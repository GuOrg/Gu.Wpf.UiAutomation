namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ISpreadsheetItemPatternProperties
    {
        PropertyId Formula { get; }

        PropertyId AnnotationObjects { get; }

        PropertyId AnnotationTypes { get; }
    }
}