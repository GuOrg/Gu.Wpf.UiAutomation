namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ISelectionItemPatternEvents
    {
        EventId ElementAddedToSelectionEvent { get; }

        EventId ElementRemovedFromSelectionEvent { get; }

        EventId ElementSelectedEvent { get; }
    }
}