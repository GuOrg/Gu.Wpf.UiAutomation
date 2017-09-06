namespace Gu.Wpf.UiAutomation
{
    public interface IDragPatternEvents
    {
        EventId DragCancelEvent { get; }

        EventId DragCompleteEvent { get; }

        EventId DragStartEvent { get; }
    }
}