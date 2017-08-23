namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IGridPatternProperties
    {
        PropertyId ColumnCount { get; }

        PropertyId RowCount { get; }
    }
}