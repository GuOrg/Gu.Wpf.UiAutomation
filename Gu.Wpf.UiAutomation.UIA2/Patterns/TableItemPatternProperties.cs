namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class TableItemPatternProperties : ITableItemPatternProperties
    {
        public PropertyId ColumnHeaderItems => TableItemPattern.ColumnHeaderItemsProperty;

        public PropertyId RowHeaderItems => TableItemPattern.RowHeaderItemsProperty;
    }
}