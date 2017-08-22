namespace Gu.Wpf.UiAutomation
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns;

    public interface IEventLibrary
    {
        IAutomationElementEvents Element { get; }

        IDragPatternEvents Drag { get; }

        IDropTargetPatternEvents DropTarget { get; }

        IInvokePatternEvents Invoke { get; }

        ISelectionItemPatternEvents SelectionItem { get; }

        ISelectionPatternEvents Selection { get; }

        ISynchronizedInputPatternEvents SynchronizedInput { get; }

        ITextEditPatternEvents TextEdit { get; }

        ITextPatternEvents Text { get; }

        IWindowPatternEvents Window { get; }
    }
}
