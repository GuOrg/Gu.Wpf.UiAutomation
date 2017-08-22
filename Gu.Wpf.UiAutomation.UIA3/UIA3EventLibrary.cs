namespace Gu.Wpf.UiAutomation.UIA3
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Patterns;

    public class UIA3EventLibrary : IEventLibrary
    {
        public UIA3EventLibrary()
        {
            Element = new UIA3AutomationElementEvents();
            Drag = new DragPatternEvents();
            DropTarget = new DropTargetPatternEvents();
            Invoke = new InvokePatternEvents();
            SelectionItem = new SelectionItemPatternEvents();
            Selection = new SelectionPatternEvents();
            SynchronizedInput = new SynchronizedInputPatternEvents();
            TextEdit = new TextEditPatternEvents();
            Text = new TextPatternEvents();
            Window = new WindowPatternEvents();
        }

        public IAutomationElementEvents Element { get; }
        public IDragPatternEvents Drag { get; }
        public IDropTargetPatternEvents DropTarget { get; }
        public IInvokePatternEvents Invoke { get; }
        public ISelectionItemPatternEvents SelectionItem { get; }
        public ISelectionPatternEvents Selection { get; }
        public ISynchronizedInputPatternEvents SynchronizedInput { get; }
        public ITextEditPatternEvents TextEdit { get; }
        public ITextPatternEvents Text { get; }
        public IWindowPatternEvents Window { get; }
    }
}
