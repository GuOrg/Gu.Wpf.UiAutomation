namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class InvokePatternBase<TNativePattern> : PatternBase<TNativePattern>, IInvokePattern
    {
        protected InvokePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IInvokePatternEvents Events => this.Automation.EventLibrary.Invoke;

        /// <inheritdoc/>
        public abstract void Invoke();
    }
}
