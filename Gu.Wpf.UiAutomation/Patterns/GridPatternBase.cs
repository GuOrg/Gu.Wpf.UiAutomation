namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class GridPatternBase<TNativePattern> : PatternBase<TNativePattern>, IGridPattern
    {
        private AutomationProperty<int> columnCount;
        private AutomationProperty<int> rowCount;

        protected GridPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public IGridPatternProperties Properties => this.Automation.PropertyLibrary.Grid;

        public AutomationProperty<int> ColumnCount => this.GetOrCreate(ref this.columnCount, this.Properties.ColumnCount);

        public AutomationProperty<int> RowCount => this.GetOrCreate(ref this.rowCount, this.Properties.RowCount);

        public abstract AutomationElement GetItem(int row, int column);
    }
}
