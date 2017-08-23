namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IAnnotationPattern : IPattern
    {
        IAnnotationPatternProperties Properties { get; }

        AutomationProperty<AnnotationType> AnnotationType { get; }

        AutomationProperty<string> AnnotationTypeName { get; }

        AutomationProperty<string> Author { get; }

        AutomationProperty<string> DateTime { get; }

        AutomationProperty<AutomationElement> Target { get; }
    }
}