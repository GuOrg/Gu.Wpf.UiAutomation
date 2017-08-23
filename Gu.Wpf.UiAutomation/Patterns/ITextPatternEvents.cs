namespace Gu.Wpf.UiAutomation
{
    public interface ITextPatternEvents
    {
        EventId TextChangedEvent { get; }

        EventId TextSelectionChangedEvent { get; }
    }
}