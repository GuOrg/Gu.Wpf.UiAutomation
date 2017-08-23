namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class DragPatternEvents : IDragPatternEvents
    {
        public EventId DragCancelEvent => DragPattern.DragCancelEvent;

        public EventId DragCompleteEvent => DragPattern.DragCompleteEvent;

        public EventId DragStartEvent => DragPattern.DragStartEvent;
    }
}