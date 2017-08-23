namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IDragPatternEvents
    {
        EventId DragCancelEvent { get; }

        EventId DragCompleteEvent { get; }

        EventId DragStartEvent { get; }
    }
}