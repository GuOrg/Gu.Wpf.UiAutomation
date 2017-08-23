namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

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
