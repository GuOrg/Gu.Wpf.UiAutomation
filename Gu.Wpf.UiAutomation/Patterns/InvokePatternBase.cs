namespace Gu.Wpf.UiAutomation
{
    public abstract class InvokePatternBase<TNativePattern> : PatternBase<TNativePattern>, IInvokePattern
        where TNativePattern : class
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
