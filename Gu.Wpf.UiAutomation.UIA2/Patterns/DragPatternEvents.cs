namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DragPatternEvents : IDragPatternEvents
    {
        public EventId DragCancelEvent => EventId.NotSupportedByFramework;

        public EventId DragCompleteEvent => EventId.NotSupportedByFramework;

        public EventId DragStartEvent => EventId.NotSupportedByFramework;
    }
}