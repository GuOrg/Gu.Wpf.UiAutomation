namespace Gu.Wpf.UiAutomation
{
    public abstract class SynchronizedInputPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISynchronizedInputPattern
    {
        protected SynchronizedInputPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ISynchronizedInputPatternEvents Events => this.Automation.EventLibrary.SynchronizedInput;

        /// <inheritdoc/>
        public abstract void Cancel();

        /// <inheritdoc/>
        public abstract void StartListening(SynchronizedInputType inputType);
    }
}
