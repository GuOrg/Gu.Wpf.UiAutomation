namespace Gu.Wpf.UiAutomation.Patterns.Infrastructure
{
    using System;
    using Gu.Wpf.UiAutomation.Identifiers;

    public abstract class PatternBase<TNativePattern> : IPattern
    {
        public BasicAutomationElementBase BasicAutomationElement { get; }

        public AutomationBase Automation => this.BasicAutomationElement.Automation;

        public TNativePattern NativePattern { get; private set; }

        protected PatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
        {
            if (basicAutomationElement == null)
            {
                throw new ArgumentNullException(nameof(basicAutomationElement));
            }

            if (nativePattern == null)
            {
                throw new ArgumentNullException(nameof(nativePattern));
            }

            this.BasicAutomationElement = basicAutomationElement;
            this.NativePattern = nativePattern;
        }

        protected AutomationProperty<T> GetOrCreate<T>(ref AutomationProperty<T> val, PropertyId propertyId)
        {
            return val ?? (val = new AutomationProperty<T>(propertyId, this.BasicAutomationElement));
        }
    }
}
