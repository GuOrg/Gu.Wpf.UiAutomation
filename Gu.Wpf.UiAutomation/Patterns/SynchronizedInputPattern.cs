namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISynchronizedInputPattern : IPattern
    {
        ISynchronizedInputPatternEvents Events { get; }

        void Cancel();
        void StartListening(SynchronizedInputType inputType);
    }

    public interface ISynchronizedInputPatternEvents
    {
        EventId DiscardedEvent { get; }
        EventId ReachedOtherElementEvent { get; }
        EventId ReachedTargetEvent { get; }
    }

    public abstract class SynchronizedInputPatternBase<TNativePattern> : PatternBase<TNativePattern>, ISynchronizedInputPattern
    {
        protected SynchronizedInputPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public ISynchronizedInputPatternEvents Events => this.Automation.EventLibrary.SynchronizedInput;

        public abstract void Cancel();
        public abstract void StartListening(SynchronizedInputType inputType);
    }
}
