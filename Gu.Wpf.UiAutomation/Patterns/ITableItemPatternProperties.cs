namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ITableItemPatternProperties
    {
        PropertyId ColumnHeaderItems { get; }

        PropertyId RowHeaderItems { get; }
    }
}