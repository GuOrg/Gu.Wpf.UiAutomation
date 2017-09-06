namespace Gu.Wpf.UiAutomation
{
    public interface IWindowPatternEvents
    {
        EventId WindowClosedEvent { get; }

        EventId WindowOpenedEvent { get; }
    }
}