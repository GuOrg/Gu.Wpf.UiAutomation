namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class GridPatternBase<TNativePattern> : PatternBase<TNativePattern>, IGridPattern
    {
        private AutomationProperty<int> columnCount;
        private AutomationProperty<int> rowCount;

        protected GridPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public IGridPatternProperties Properties => this.Automation.PropertyLibrary.Grid;

        /// <inheritdoc/>
        public AutomationProperty<int> ColumnCount => this.GetOrCreate(ref this.columnCount, this.Properties.ColumnCount);

        /// <inheritdoc/>
        public AutomationProperty<int> RowCount => this.GetOrCreate(ref this.rowCount, this.Properties.RowCount);

        /// <inheritdoc/>
        public abstract AutomationElement GetItem(int row, int column);
    }
}
