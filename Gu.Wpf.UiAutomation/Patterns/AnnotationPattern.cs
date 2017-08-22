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
        private AutomationProperty<AnnotationType> annotationType;
        private AutomationProperty<string> annotationTypeName;
        private AutomationProperty<string> author;
        private AutomationProperty<string> dateTime;
        private AutomationProperty<AutomationElement> target;

        protected AnnotationPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IAnnotationPatternProperties Properties => this.Automation.PropertyLibrary.Annotation;

        public AutomationProperty<AnnotationType> AnnotationType => this.GetOrCreate(ref this.annotationType, this.Properties.AnnotationTypeId);
        public AutomationProperty<string> AnnotationTypeName => this.GetOrCreate(ref this.annotationTypeName, this.Properties.AnnotationTypeName);
        public AutomationProperty<string> Author => this.GetOrCreate(ref this.author, this.Properties.Author);
        public AutomationProperty<string> DateTime => this.GetOrCreate(ref this.dateTime, this.Properties.DateTime);
        public AutomationProperty<AutomationElement> Target => this.GetOrCreate(ref this.target, this.Properties.Target);
    }
}
