namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class TableItemPatternProperties : ITableItemPatternProperties
    {
        public PropertyId ColumnHeaderItems => TableItemPattern.ColumnHeaderItemsProperty;

        public PropertyId RowHeaderItems => TableItemPattern.RowHeaderItemsProperty;
    }
}