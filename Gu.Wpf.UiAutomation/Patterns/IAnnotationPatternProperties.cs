namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IAnnotationPatternProperties
    {
        PropertyId AnnotationTypeId { get; }

        PropertyId AnnotationTypeName { get; }

        PropertyId Author { get; }

        PropertyId DateTime { get; }

        PropertyId Target { get; }
    }
}