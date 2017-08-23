namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DropTargetPatternEvents : IDropTargetPatternEvents
    {
        public EventId DragEnterEvent => EventId.NotSupportedByFramework;

        public EventId DragLeaveEvent => EventId.NotSupportedByFramework;

        public EventId DragCompleteEvent => EventId.NotSupportedByFramework;
    }
}