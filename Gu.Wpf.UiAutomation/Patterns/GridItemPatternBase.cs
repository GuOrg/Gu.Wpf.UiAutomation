namespace Gu.Wpf.UiAutomation
{
    public abstract class GridItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, IGridItemPattern
    {
        private AutomationProperty<int> column;
        private AutomationProperty<int> columnSpan;
        private AutomationProperty<AutomationElement> containingGrid;
        private AutomationProperty<int> row;
        private AutomationProperty<int> rowSpan;

        protected GridItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IGridItemPatternProperties Properties => this.Automation.PropertyLibrary.GridItem;

        /// <inheritdoc/>
        public AutomationProperty<int> Column => this.GetOrCreate(ref this.column, this.Properties.Column);

        /// <inheritdoc/>
        public AutomationProperty<int> ColumnSpan => this.GetOrCreate(ref this.columnSpan, this.Properties.ColumnSpan);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement> ContainingGrid => this.GetOrCreate(ref this.containingGrid, this.Properties.ContainingGrid);

        /// <inheritdoc/>
        public AutomationProperty<int> Row => this.GetOrCreate(ref this.row, this.Properties.Row);

        /// <inheritdoc/>
        public AutomationProperty<int> RowSpan => this.GetOrCreate(ref this.rowSpan, this.Properties.RowSpan);
    }
}
