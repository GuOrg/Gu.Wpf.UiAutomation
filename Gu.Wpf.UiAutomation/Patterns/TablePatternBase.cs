﻿namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
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

        public ITablePatternProperties Properties => this.Automation.PropertyLibrary.Table;

        public AutomationProperty<AutomationElement[]> ColumnHeaders => this.GetOrCreate(ref this.columnHeaders, this.Properties.ColumnHeaders);

        public AutomationProperty<AutomationElement[]> RowHeaders => this.GetOrCreate(ref this.rowHeaders, this.Properties.RowHeaders);

        public AutomationProperty<RowOrColumnMajor> RowOrColumnMajor => this.GetOrCreate(ref this.rowOrColumnMajor, this.Properties.RowOrColumnMajor);
    }
}