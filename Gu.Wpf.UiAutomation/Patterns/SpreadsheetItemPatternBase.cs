namespace Gu.Wpf.UiAutomation
{
    public abstract class SpreadsheetItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISpreadsheetItemPattern
        where TNativePattern : class
    {
        private AutomationProperty<string> formula;
        private AutomationProperty<AutomationElement[]> annotationObjects;
        private AutomationProperty<AnnotationType[]> annotationTypes;

        protected SpreadsheetItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ISpreadsheetItemPatternProperties Properties => this.Automation.PropertyLibrary.SpreadsheetItem;

        /// <inheritdoc/>
        public AutomationProperty<string> Formula => this.GetOrCreate(ref this.formula, this.Properties.Formula);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> AnnotationObjects => this.GetOrCreate(ref this.annotationObjects, this.Properties.AnnotationObjects);

        /// <inheritdoc/>
        public AutomationProperty<AnnotationType[]> AnnotationTypes => this.GetOrCreate(ref this.annotationTypes, this.Properties.AnnotationTypes);
    }
}
