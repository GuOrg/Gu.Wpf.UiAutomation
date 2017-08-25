namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public abstract class TableItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, ITableItemPattern
        where TNativePattern : class
    {
        private AutomationProperty<IReadOnlyList<AutomationElement>> columnHeaderItems;
        private AutomationProperty<IReadOnlyList<AutomationElement>> rowHeaderItems;

        protected TableItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ITableItemPatternProperties Properties => this.Automation.PropertyLibrary.TableItem;

        /// <inheritdoc/>
        public AutomationProperty<IReadOnlyList<AutomationElement>> ColumnHeaderItems => this.GetOrCreate(ref this.columnHeaderItems, this.Properties.ColumnHeaderItems);

        /// <inheritdoc/>
        public AutomationProperty<IReadOnlyList<AutomationElement>> RowHeaderItems => this.GetOrCreate(ref this.rowHeaderItems, this.Properties.RowHeaderItems);
    }
}
