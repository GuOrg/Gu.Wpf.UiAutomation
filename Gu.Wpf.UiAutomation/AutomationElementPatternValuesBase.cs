namespace Gu.Wpf.UiAutomation
{
    using System;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class AutomationElementPatternValuesBase
    {
        private IAutomationPattern<IAnnotationPattern> annotationPattern;
        private IAutomationPattern<IDockPattern> dockPattern;
        private IAutomationPattern<IDragPattern> dragPattern;
        private IAutomationPattern<IDropTargetPattern> dropTargetPattern;
        private IAutomationPattern<IExpandCollapsePattern> expandCollapsePattern;
        private IAutomationPattern<IGridItemPattern> gridItemPattern;
        private IAutomationPattern<IGridPattern> gridPattern;
        private IAutomationPattern<IInvokePattern> invokePattern;
        private IAutomationPattern<IItemContainerPattern> itemContainerPattern;
        private IAutomationPattern<ILegacyIAccessiblePattern> legacyIAccessiblePattern;
        private IAutomationPattern<IMultipleViewPattern> multipleViewPattern;
        private IAutomationPattern<IObjectModelPattern> objectModelPattern;
        private IAutomationPattern<IRangeValuePattern> rangeValuePattern;
        private IAutomationPattern<IScrollItemPattern> scrollItemPattern;
        private IAutomationPattern<IScrollPattern> scrollPattern;
        private IAutomationPattern<ISelectionItemPattern> selectionItemPattern;
        private IAutomationPattern<ISelectionPattern> selectionPattern;
        private IAutomationPattern<ISpreadsheetItemPattern> spreadsheetItemPattern;
        private IAutomationPattern<ISpreadsheetPattern> spreadsheetPattern;
        private IAutomationPattern<IStylesPattern> stylesPattern;
        private IAutomationPattern<ISynchronizedInputPattern> synchronizedInputPattern;
        private IAutomationPattern<ITableItemPattern> tableItemPattern;
        private IAutomationPattern<ITablePattern> tablePattern;
        private IAutomationPattern<ITextChildPattern> textChildPattern;
        private IAutomationPattern<ITextEditPattern> textEditPattern;
        private IAutomationPattern<IText2Pattern> text2Pattern;
        private IAutomationPattern<ITextPattern> textPattern;
        private IAutomationPattern<ITogglePattern> togglePattern;
        private IAutomationPattern<ITransform2Pattern> transform2Pattern;
        private IAutomationPattern<ITransformPattern> transformPattern;
        private IAutomationPattern<IValuePattern> valuePattern;
        private IAutomationPattern<IVirtualizedItemPattern> virtualizedItemPattern;
        private IAutomationPattern<IWindowPattern> windowPattern;

        protected AutomationElementPatternValuesBase(BasicAutomationElementBase basicAutomationElement)
        {
            this.BasicAutomationElement = basicAutomationElement;
        }

        protected BasicAutomationElementBase BasicAutomationElement { get; }

        public IAutomationPattern<IAnnotationPattern> Annotation => this.GetOrCreate(ref this.annotationPattern, this.InitializeAnnotationPattern);

        public IAutomationPattern<IDockPattern> Dock => this.GetOrCreate(ref this.dockPattern, this.InitializeDockPattern);

        public IAutomationPattern<IDragPattern> Drag => this.GetOrCreate(ref this.dragPattern, this.InitializeDragPattern);

        public IAutomationPattern<IDropTargetPattern> DropTarget => this.GetOrCreate(ref this.dropTargetPattern, this.InitializeDropTargetPattern);

        public IAutomationPattern<IExpandCollapsePattern> ExpandCollapse => this.GetOrCreate(ref this.expandCollapsePattern, this.InitializeExpandCollapsePattern);

        public IAutomationPattern<IGridItemPattern> GridItem => this.GetOrCreate(ref this.gridItemPattern, this.InitializeGridItemPattern);

        public IAutomationPattern<IGridPattern> Grid => this.GetOrCreate(ref this.gridPattern, this.InitializeGridPattern);

        public IAutomationPattern<IInvokePattern> Invoke => this.GetOrCreate(ref this.invokePattern, this.InitializeInvokePattern);

        public IAutomationPattern<IItemContainerPattern> ItemContainer => this.GetOrCreate(ref this.itemContainerPattern, this.InitializeItemContainerPattern);

        public IAutomationPattern<ILegacyIAccessiblePattern> LegacyIAccessible => this.GetOrCreate(ref this.legacyIAccessiblePattern, this.InitializeLegacyIAccessiblePattern);

        public IAutomationPattern<IMultipleViewPattern> MultipleView => this.GetOrCreate(ref this.multipleViewPattern, this.InitializeMultipleViewPattern);

        public IAutomationPattern<IObjectModelPattern> ObjectModel => this.GetOrCreate(ref this.objectModelPattern, this.InitializeObjectModelPattern);

        public IAutomationPattern<IRangeValuePattern> RangeValue => this.GetOrCreate(ref this.rangeValuePattern, this.InitializeRangeValuePattern);

        public IAutomationPattern<IScrollItemPattern> ScrollItem => this.GetOrCreate(ref this.scrollItemPattern, this.InitializeScrollItemPattern);

        public IAutomationPattern<IScrollPattern> Scroll => this.GetOrCreate(ref this.scrollPattern, this.InitializeScrollPattern);

        public IAutomationPattern<ISelectionItemPattern> SelectionItem => this.GetOrCreate(ref this.selectionItemPattern, this.InitializeSelectionItemPattern);

        public IAutomationPattern<ISelectionPattern> Selection => this.GetOrCreate(ref this.selectionPattern, this.InitializeSelectionPattern);

        public IAutomationPattern<ISpreadsheetItemPattern> SpreadsheetItem => this.GetOrCreate(ref this.spreadsheetItemPattern, this.InitializeSpreadsheetItemPattern);

        public IAutomationPattern<ISpreadsheetPattern> Spreadsheet => this.GetOrCreate(ref this.spreadsheetPattern, this.InitializeSpreadsheetPattern);

        public IAutomationPattern<IStylesPattern> Styles => this.GetOrCreate(ref this.stylesPattern, this.InitializeStylesPattern);

        public IAutomationPattern<ISynchronizedInputPattern> SynchronizedInput => this.GetOrCreate(ref this.synchronizedInputPattern, this.InitializeSynchronizedInputPattern);

        public IAutomationPattern<ITableItemPattern> TableItem => this.GetOrCreate(ref this.tableItemPattern, this.InitializeTableItemPattern);

        public IAutomationPattern<ITablePattern> Table => this.GetOrCreate(ref this.tablePattern, this.InitializeTablePattern);

        public IAutomationPattern<ITextChildPattern> TextChild => this.GetOrCreate(ref this.textChildPattern, this.InitializeTextChildPattern);

        public IAutomationPattern<ITextEditPattern> TextEdit => this.GetOrCreate(ref this.textEditPattern, this.InitializeTextEditPattern);

        public IAutomationPattern<IText2Pattern> Text2 => this.GetOrCreate(ref this.text2Pattern, this.InitializeText2Pattern);

        public IAutomationPattern<ITextPattern> Text => this.GetOrCreate(ref this.textPattern, this.InitializeTextPattern);

        public IAutomationPattern<ITogglePattern> Toggle => this.GetOrCreate(ref this.togglePattern, this.InitializeTogglePattern);

        public IAutomationPattern<ITransform2Pattern> Transform2 => this.GetOrCreate(ref this.transform2Pattern, this.InitializeTransform2Pattern);

        public IAutomationPattern<ITransformPattern> Transform => this.GetOrCreate(ref this.transformPattern, this.InitializeTransformPattern);

        public IAutomationPattern<IValuePattern> Value => this.GetOrCreate(ref this.valuePattern, this.InitializeValuePattern);

        public IAutomationPattern<IVirtualizedItemPattern> VirtualizedItem => this.GetOrCreate(ref this.virtualizedItemPattern, this.InitializeVirtualizedItemPattern);

        public IAutomationPattern<IWindowPattern> Window => this.GetOrCreate(ref this.windowPattern, this.InitializeWindowPattern);

        protected abstract IAutomationPattern<IAnnotationPattern> InitializeAnnotationPattern();

        protected abstract IAutomationPattern<IDockPattern> InitializeDockPattern();

        protected abstract IAutomationPattern<IDragPattern> InitializeDragPattern();

        protected abstract IAutomationPattern<IDropTargetPattern> InitializeDropTargetPattern();

        protected abstract IAutomationPattern<IExpandCollapsePattern> InitializeExpandCollapsePattern();

        protected abstract IAutomationPattern<IGridItemPattern> InitializeGridItemPattern();

        protected abstract IAutomationPattern<IGridPattern> InitializeGridPattern();

        protected abstract IAutomationPattern<IInvokePattern> InitializeInvokePattern();

        protected abstract IAutomationPattern<IItemContainerPattern> InitializeItemContainerPattern();

        protected abstract IAutomationPattern<ILegacyIAccessiblePattern> InitializeLegacyIAccessiblePattern();

        protected abstract IAutomationPattern<IMultipleViewPattern> InitializeMultipleViewPattern();

        protected abstract IAutomationPattern<IObjectModelPattern> InitializeObjectModelPattern();

        protected abstract IAutomationPattern<IRangeValuePattern> InitializeRangeValuePattern();

        protected abstract IAutomationPattern<IScrollItemPattern> InitializeScrollItemPattern();

        protected abstract IAutomationPattern<IScrollPattern> InitializeScrollPattern();

        protected abstract IAutomationPattern<ISelectionItemPattern> InitializeSelectionItemPattern();

        protected abstract IAutomationPattern<ISelectionPattern> InitializeSelectionPattern();

        protected abstract IAutomationPattern<ISpreadsheetItemPattern> InitializeSpreadsheetItemPattern();

        protected abstract IAutomationPattern<ISpreadsheetPattern> InitializeSpreadsheetPattern();

        protected abstract IAutomationPattern<IStylesPattern> InitializeStylesPattern();

        protected abstract IAutomationPattern<ISynchronizedInputPattern> InitializeSynchronizedInputPattern();

        protected abstract IAutomationPattern<ITableItemPattern> InitializeTableItemPattern();

        protected abstract IAutomationPattern<ITablePattern> InitializeTablePattern();

        protected abstract IAutomationPattern<ITextChildPattern> InitializeTextChildPattern();

        protected abstract IAutomationPattern<ITextEditPattern> InitializeTextEditPattern();

        protected abstract IAutomationPattern<IText2Pattern> InitializeText2Pattern();

        protected abstract IAutomationPattern<ITextPattern> InitializeTextPattern();

        protected abstract IAutomationPattern<ITogglePattern> InitializeTogglePattern();

        protected abstract IAutomationPattern<ITransform2Pattern> InitializeTransform2Pattern();

        protected abstract IAutomationPattern<ITransformPattern> InitializeTransformPattern();

        protected abstract IAutomationPattern<IValuePattern> InitializeValuePattern();

        protected abstract IAutomationPattern<IVirtualizedItemPattern> InitializeVirtualizedItemPattern();

        protected abstract IAutomationPattern<IWindowPattern> InitializeWindowPattern();

        private IAutomationPattern<T> GetOrCreate<T>(ref IAutomationPattern<T> val, Func<IAutomationPattern<T>> initFunc)
            where T : IPattern
        {
            return val ?? (val = initFunc());
        }

        public abstract IAutomationPattern<T> GetCustomPattern<T, TNative>(PatternId pattern, Func<BasicAutomationElementBase, TNative, T> patternCreateFunc)
            where T : IPattern;
    }
}
