namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DragPatternEvents : IDragPatternEvents
    {
        public EventId DragCancelEvent => DragPattern.DragCancelEvent;

        public EventId DragCompleteEvent => DragPattern.DragCompleteEvent;

        public EventId DragStartEvent => DragPattern.DragStartEvent;
    }
}