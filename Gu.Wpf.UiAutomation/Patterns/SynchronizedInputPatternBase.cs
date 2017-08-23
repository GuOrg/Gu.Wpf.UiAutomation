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

        public ISynchronizedInputPatternEvents Events => this.Automation.EventLibrary.SynchronizedInput;

        public abstract void Cancel();

        public abstract void StartListening(SynchronizedInputType inputType);
    }
}
