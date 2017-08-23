namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class TableItemPatternProperties : ITableItemPatternProperties
    {
        public PropertyId ColumnHeaderItems => TableItemPattern.ColumnHeaderItemsProperty;

        public PropertyId RowHeaderItems => TableItemPattern.RowHeaderItemsProperty;
    }
}