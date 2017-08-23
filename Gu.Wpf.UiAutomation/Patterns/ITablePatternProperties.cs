namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ITablePatternProperties
    {
        PropertyId ColumnHeaders { get; }

        PropertyId RowHeaders { get; }

        PropertyId RowOrColumnMajor { get; }
    }
}