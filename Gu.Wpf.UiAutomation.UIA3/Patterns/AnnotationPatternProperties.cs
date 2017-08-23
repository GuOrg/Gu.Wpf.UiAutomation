namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class AnnotationPatternProperties : IAnnotationPatternProperties
    {
        public PropertyId AnnotationTypeId => AnnotationPattern.AnnotationTypeIdProperty;

        public PropertyId AnnotationTypeName => AnnotationPattern.AnnotationTypeNameProperty;

        public PropertyId Author => AnnotationPattern.AuthorProperty;

        public PropertyId DateTime => AnnotationPattern.DateTimeProperty;

        public PropertyId Target => AnnotationPattern.TargetProperty;
    }
}