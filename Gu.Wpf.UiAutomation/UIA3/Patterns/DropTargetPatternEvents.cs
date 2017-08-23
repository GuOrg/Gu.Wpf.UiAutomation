namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class DropTargetPatternEvents : IDropTargetPatternEvents
    {
        public EventId DragEnterEvent => DropTargetPattern.DragEnterEvent;

        public EventId DragLeaveEvent => DropTargetPattern.DragLeaveEvent;

        public EventId DragCompleteEvent => DropTargetPattern.DragCompleteEvent;
    }
}