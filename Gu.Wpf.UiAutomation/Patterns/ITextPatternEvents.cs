namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ITextPatternEvents
    {
        EventId TextChangedEvent { get; }

        EventId TextSelectionChangedEvent { get; }
    }
}