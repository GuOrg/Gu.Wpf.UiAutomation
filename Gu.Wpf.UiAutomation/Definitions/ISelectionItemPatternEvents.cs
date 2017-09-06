namespace Gu.Wpf.UiAutomation
{
    public interface ISelectionItemPatternEvents
    {
        EventId ElementAddedToSelectionEvent { get; }

        EventId ElementRemovedFromSelectionEvent { get; }

        EventId ElementSelectedEvent { get; }
    }
}