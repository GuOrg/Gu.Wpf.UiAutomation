namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IGridItemPattern : IPattern
    {
        IGridItemPatternProperties Properties { get; }

        AutomationProperty<int> Column { get; }
        AutomationProperty<int> ColumnSpan { get; }
        AutomationProperty<AutomationElement> ContainingGrid { get; }
        AutomationProperty<int> Row { get; }
        AutomationProperty<int> RowSpan { get; }
    }

    public interface IGridItemPatternProperties
    {
        PropertyId Column { get; }
        PropertyId ColumnSpan { get; }
        PropertyId ContainingGrid { get; }
        PropertyId Row { get; }
        PropertyId RowSpan { get; }
    }

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

        public IGridItemPatternProperties Properties => this.Automation.PropertyLibrary.GridItem;

        public AutomationProperty<int> Column => this.GetOrCreate(ref this.column, this.Properties.Column);
        public AutomationProperty<int> ColumnSpan => this.GetOrCreate(ref this.columnSpan, this.Properties.ColumnSpan);
        public AutomationProperty<AutomationElement> ContainingGrid => this.GetOrCreate(ref this.containingGrid, this.Properties.ContainingGrid);
        public AutomationProperty<int> Row => this.GetOrCreate(ref this.row, this.Properties.Row);
        public AutomationProperty<int> RowSpan => this.GetOrCreate(ref this.rowSpan, this.Properties.RowSpan);
    }
}
