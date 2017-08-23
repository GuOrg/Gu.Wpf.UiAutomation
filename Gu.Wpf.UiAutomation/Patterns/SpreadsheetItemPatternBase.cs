namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class SpreadsheetItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISpreadsheetItemPattern
    {
        private AutomationProperty<string> formula;
        private AutomationProperty<AutomationElement[]> annotationObjects;
        private AutomationProperty<AnnotationType[]> annotationTypes;

        protected SpreadsheetItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public ISpreadsheetItemPatternProperties Properties => this.Automation.PropertyLibrary.SpreadsheetItem;

        public AutomationProperty<string> Formula => this.GetOrCreate(ref this.formula, this.Properties.Formula);

        public AutomationProperty<AutomationElement[]> AnnotationObjects => this.GetOrCreate(ref this.annotationObjects, this.Properties.AnnotationObjects);

        public AutomationProperty<AnnotationType[]> AnnotationTypes => this.GetOrCreate(ref this.annotationTypes, this.Properties.AnnotationTypes);
    }
}
