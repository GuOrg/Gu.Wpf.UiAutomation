namespace Gu.Wpf.UiAutomation
{
    public interface ITableItemPattern : IPattern
    {
        ITableItemPatternProperties Properties { get; }

        AutomationProperty<AutomationElement[]> ColumnHeaderItems { get; }

        AutomationProperty<AutomationElement[]> RowHeaderItems { get; }
    }
}