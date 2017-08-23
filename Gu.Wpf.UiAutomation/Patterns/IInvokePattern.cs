namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IInvokePattern : IPattern
    {
        IInvokePatternEvents Events { get; }

        void Invoke();
    }
}