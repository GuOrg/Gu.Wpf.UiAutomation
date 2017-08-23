namespace Gu.Wpf.UiAutomation
{
    public abstract class TableItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, ITableItemPattern
    {
        private AutomationProperty<AutomationElement[]> columnHeaderItems;
        private AutomationProperty<AutomationElement[]> rowHeaderItems;

        protected TableItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ITableItemPatternProperties Properties => this.Automation.PropertyLibrary.TableItem;

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> ColumnHeaderItems => this.GetOrCreate(ref this.columnHeaderItems, this.Properties.ColumnHeaderItems);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> RowHeaderItems => this.GetOrCreate(ref this.rowHeaderItems, this.Properties.RowHeaderItems);
    }
}
