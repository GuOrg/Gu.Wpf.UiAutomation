namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class TablePatternProperties : ITablePatternProperties
    {
        public PropertyId ColumnHeaders => TablePattern.ColumnHeadersProperty;

        public PropertyId RowHeaders => TablePattern.RowHeadersProperty;

        public PropertyId RowOrColumnMajor => TablePattern.RowOrColumnMajorProperty;
    }
}