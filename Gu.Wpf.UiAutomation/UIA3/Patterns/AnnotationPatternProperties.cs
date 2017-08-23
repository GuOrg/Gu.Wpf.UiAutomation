namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class AnnotationPatternProperties : IAnnotationPatternProperties
    {
        public PropertyId AnnotationTypeId => AnnotationPattern.AnnotationTypeIdProperty;

        public PropertyId AnnotationTypeName => AnnotationPattern.AnnotationTypeNameProperty;

        public PropertyId Author => AnnotationPattern.AuthorProperty;

        public PropertyId DateTime => AnnotationPattern.DateTimeProperty;

        public PropertyId Target => AnnotationPattern.TargetProperty;
    }
}