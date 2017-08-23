namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public abstract class TablePatternBase<TNativePattern> : PatternBase<TNativePattern>, ITablePattern
    {
        private AutomationProperty<AutomationElement[]> columnHeaders;
        private AutomationProperty<AutomationElement[]> rowHeaders;
        private AutomationProperty<RowOrColumnMajor> rowOrColumnMajor;

        protected TablePatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        /// <inheritdoc/>
        public ITablePatternProperties Properties => this.Automation.PropertyLibrary.Table;

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> ColumnHeaders => this.GetOrCreate(ref this.columnHeaders, this.Properties.ColumnHeaders);

        /// <inheritdoc/>
        public AutomationProperty<AutomationElement[]> RowHeaders => this.GetOrCreate(ref this.rowHeaders, this.Properties.RowHeaders);

        /// <inheritdoc/>
        public AutomationProperty<RowOrColumnMajor> RowOrColumnMajor => this.GetOrCreate(ref this.rowOrColumnMajor, this.Properties.RowOrColumnMajor);
    }
}
