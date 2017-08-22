namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ITableItemPattern : IPattern
    {
        ITableItemPatternProperties Properties { get; }

        AutomationProperty<AutomationElement[]> ColumnHeaderItems { get; }
        AutomationProperty<AutomationElement[]> RowHeaderItems { get; }
    }

    public interface ITableItemPatternProperties
    {
        PropertyId ColumnHeaderItems { get; }
        PropertyId RowHeaderItems { get; }
    }

    public abstract class TableItemPatternBase<TNativePattern> : PatternBase<TNativePattern>, ITableItemPattern
    {
        private AutomationProperty<AutomationElement[]> columnHeaderItems;
        private AutomationProperty<AutomationElement[]> rowHeaderItems;

        protected TableItemPatternBase(BasicAutomationElementBase basicAutomationElement, TNativePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public ITableItemPatternProperties Properties => this.Automation.PropertyLibrary.TableItem;

        public AutomationProperty<AutomationElement[]> ColumnHeaderItems => this.GetOrCreate(ref this.columnHeaderItems, this.Properties.ColumnHeaderItems);
        public AutomationProperty<AutomationElement[]> RowHeaderItems => this.GetOrCreate(ref this.rowHeaderItems, this.Properties.RowHeaderItems);
    }
}
