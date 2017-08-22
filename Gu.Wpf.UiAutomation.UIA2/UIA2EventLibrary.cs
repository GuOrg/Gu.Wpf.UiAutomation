namespace Gu.Wpf.UiAutomation.UIA2
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA2.Patterns;

    public class UIA2EventLibrary : IEventLibrary
    {
        public UIA2EventLibrary()
        {
            Element = new UIA2AutomationElementEvents();
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
