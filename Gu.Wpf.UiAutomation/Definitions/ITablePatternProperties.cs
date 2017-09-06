namespace Gu.Wpf.UiAutomation
{
    public interface ITablePatternProperties
    {
        PropertyId ColumnHeaders { get; }

        PropertyId RowHeaders { get; }

        PropertyId RowOrColumnMajor { get; }
    }
}