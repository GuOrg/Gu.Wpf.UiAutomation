namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
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

    public interface IAnnotationPatternProperties
    {
        PropertyId AnnotationTypeId { get; }
        PropertyId AnnotationTypeName { get; }
        PropertyId Author { get; }
        PropertyId DateTime { get; }
        PropertyId Target { get; }
    }

    public abstract class AnnotationPatternBase<TNativePattern> : PatternBase<TNativePattern>, IAnnotationPattern
    {
        private AutomationProperty<AnnotationType> _annotationType;
        private AutomationProperty<string> _annotationTypeName;
        private AutomationProperty<string> _author;
        private AutomationProperty<string> _dateTime;
        private AutomationProperty<AutomationElement> _target;

        protected AnnotationPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public IAnnotationPatternProperties Properties => Automation.PropertyLibrary.Annotation;

        public AutomationProperty<AnnotationType> AnnotationType => GetOrCreate(ref _annotationType, Properties.AnnotationTypeId);
        public AutomationProperty<string> AnnotationTypeName => GetOrCreate(ref _annotationTypeName, Properties.AnnotationTypeName);
        public AutomationProperty<string> Author => GetOrCreate(ref _author, Properties.Author);
        public AutomationProperty<string> DateTime => GetOrCreate(ref _dateTime, Properties.DateTime);
        public AutomationProperty<AutomationElement> Target => GetOrCreate(ref _target, Properties.Target);
    }
}
