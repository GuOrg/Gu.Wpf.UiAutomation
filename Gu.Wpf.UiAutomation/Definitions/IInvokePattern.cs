namespace Gu.Wpf.UiAutomation
{
    public interface IInvokePattern : IPattern
    {
        IInvokePatternEvents Events { get; }

        void Invoke();
    }
}