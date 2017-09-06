namespace Gu.Wpf.UiAutomation
{
    public interface IDropTargetPatternEvents
    {
        EventId DragEnterEvent { get; }

        EventId DragLeaveEvent { get; }

        EventId DragCompleteEvent { get; }
    }
}