namespace Gu.Wpf.UiAutomation
{
    public interface ISynchronizedInputPattern : IPattern
    {
        ISynchronizedInputPatternEvents Events { get; }

        void Cancel();

        void StartListening(SynchronizedInputType inputType);
    }
}