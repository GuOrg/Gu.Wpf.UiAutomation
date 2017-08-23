namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IDropTargetPatternEvents
    {
        EventId DragEnterEvent { get; }

        EventId DragLeaveEvent { get; }

        EventId DragCompleteEvent { get; }
    }
}