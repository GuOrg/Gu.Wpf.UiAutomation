namespace Gu.Wpf.UiAutomation
{
    public interface ITablePattern : IPattern
    {
        ITablePatternProperties Properties { get; }

        AutomationProperty<AutomationElement[]> ColumnHeaders { get; }

        AutomationProperty<AutomationElement[]> RowHeaders { get; }

        AutomationProperty<RowOrColumnMajor> RowOrColumnMajor { get; }
    }
}