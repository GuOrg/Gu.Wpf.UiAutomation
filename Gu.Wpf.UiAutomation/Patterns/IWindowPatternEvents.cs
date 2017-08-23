namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IWindowPatternEvents
    {
        EventId WindowClosedEvent { get; }

        EventId WindowOpenedEvent { get; }
    }
}