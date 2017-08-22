namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IInvokePattern : IPattern
    {
        IInvokePatternEvents Events { get; }

        void Invoke();
    }

    public interface IInvokePatternEvents
    {
        EventId InvokedEvent { get; }
    }

    public abstract class InvokePatternBase<TNativePattern> : PatternBase<TNativePattern>, IInvokePattern
    {
        protected InvokePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public IInvokePatternEvents Events => this.Automation.EventLibrary.Invoke;

        public abstract void Invoke();
    }
}
