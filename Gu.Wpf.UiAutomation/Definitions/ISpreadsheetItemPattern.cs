namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public interface ISpreadsheetItemPattern : IPattern
    {
        ISpreadsheetItemPatternProperties Properties { get; }

        AutomationProperty<string> Formula { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> AnnotationObjects { get; }

        AutomationProperty<AnnotationType[]> AnnotationTypes { get; }
    }
}