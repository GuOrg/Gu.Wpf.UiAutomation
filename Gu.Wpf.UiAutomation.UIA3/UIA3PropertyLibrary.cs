namespace Gu.Wpf.UiAutomation.UIA3
{
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Patterns;

    public class UIA3PropertyLibrary : IPropertyLibray
    {
        public UIA3PropertyLibrary()
        {
            this.PatternAvailability = new UIA3AutomationElementPatternAvailabilityProperties();
            this.Element = new UIA3AutomationElementProperties();
            this.Annotation = new AnnotationPatternProperties();
            this.Dock = new DockPatternProperties();
            this.Drag = new DragPatternProperties();
            this.DropTarget = new DropTargetPatternProperties();
            this.ExpandCollapse = new ExpandCollapsePatternProperties();
            this.GridItem = new GridItemPatternProperties();
            this.Grid = new GridPatternProperties();
            this.LegacyIAccessible = new LegacyIAccessiblePatternProperties();
            this.MultipleView = new MultipleViewPatternProperties();
            this.RangeValue = new RangeValuePatternProperties();
            this.Scroll = new ScrollPatternProperties();
            this.SelectionItem = new SelectionItemPatternProperties();
            this.Selection = new SelectionPatternProperties();
            this.SpreadsheetItem = new SpreadsheetItemPatternProperties();
            this.Styles = new StylesPatternProperties();
            this.TableItem = new TableItemPatternProperties();
            this.Table = new TablePatternProperties();
            this.Toggle = new TogglePatternProperties();
            this.Transform2 = new Transform2PatternProperties();
            this.Transform = new TransformPatternProperties();
            this.Value = new ValuePatternProperties();
            this.Window = new WindowPatternProperties();
        }

        public IAutomationElementPatternAvailabilityProperties PatternAvailability { get; }

        public IAutomationElementProperties Element { get; }

        public IAnnotationPatternProperties Annotation { get; }

        public IDockPatternProperties Dock { get; }

        public IDragPatternProperties Drag { get; }

        public IDropTargetPatternProperties DropTarget { get; }

        public IExpandCollapsePatternProperties ExpandCollapse { get; }

        public IGridItemPatternProperties GridItem { get; }

        public IGridPatternProperties Grid { get; }

        public ILegacyIAccessiblePatternProperties LegacyIAccessible { get; }

        public IMultipleViewPatternProperties MultipleView { get; }

        public IRangeValuePatternProperties RangeValue { get; }

        public IScrollPatternProperties Scroll { get; }

        public ISelectionItemPatternProperties SelectionItem { get; }

        public ISelectionPatternProperties Selection { get; }

        public ISpreadsheetItemPatternProperties SpreadsheetItem { get; }

        public IStylesPatternProperties Styles { get; }

        public ITableItemPatternProperties TableItem { get; }

        public ITablePatternProperties Table { get; }

        public ITogglePatternProperties Toggle { get; }

        public ITransform2PatternProperties Transform2 { get; }

        public ITransformPatternProperties Transform { get; }

        public IValuePatternProperties Value { get; }

        public IWindowPatternProperties Window { get; }
    }
}
