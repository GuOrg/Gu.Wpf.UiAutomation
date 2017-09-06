namespace Gu.Wpf.UiAutomation
{
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