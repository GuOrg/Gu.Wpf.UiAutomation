namespace Gu.Wpf.UiAutomation
{
    using System;

    public abstract class PatternBase<TNativePattern> : IPattern
        where TNativePattern : class
    {
        protected PatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
        {
            this.BasicAutomationElement = basicAutomationElement ?? throw new ArgumentNullException(nameof(basicAutomationElement));
            this.NativePattern = nativePattern ?? throw new ArgumentNullException(nameof(nativePattern));
        }

        public BasicAutomationElementBase BasicAutomationElement { get; }

        public TNativePattern NativePattern { get; }

        public AutomationBase Automation => this.BasicAutomationElement.Automation;

        protected AutomationProperty<T> GetOrCreate<T>(ref AutomationProperty<T> val, PropertyId propertyId)
        {
            return val ?? (val = new AutomationProperty<T>(propertyId, this.BasicAutomationElement));
        }
    }
}
