namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISynchronizedInputPattern : IPattern
    {
        ISynchronizedInputPatternEvents Events { get; }

        void Cancel();

        void StartListening(SynchronizedInputType inputType);
    }
}