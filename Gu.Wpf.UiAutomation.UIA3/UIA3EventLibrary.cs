namespace Gu.Wpf.UiAutomation.UIA3
{
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Patterns;

    public class UIA3EventLibrary : IEventLibrary
    {
        public UIA3EventLibrary()
        {
            this.Element = new UIA3AutomationElementEvents();
            this.Drag = new DragPatternEvents();
            this.DropTarget = new DropTargetPatternEvents();
            this.Invoke = new InvokePatternEvents();
            this.SelectionItem = new SelectionItemPatternEvents();
            this.Selection = new SelectionPatternEvents();
            this.SynchronizedInput = new SynchronizedInputPatternEvents();
            this.TextEdit = new TextEditPatternEvents();
            this.Text = new TextPatternEvents();
            this.Window = new WindowPatternEvents();
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
