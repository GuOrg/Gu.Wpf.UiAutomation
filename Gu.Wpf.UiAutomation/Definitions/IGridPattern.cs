namespace Gu.Wpf.UiAutomation
{
    public interface IGridPattern : IPattern
    {
        IGridPatternProperties Properties { get; }

        AutomationProperty<int> ColumnCount { get; }

        AutomationProperty<int> RowCount { get; }

        AutomationElement GetItem(int row, int column);
    }
}