namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class TablePatternProperties : ITablePatternProperties
    {
        public PropertyId ColumnHeaders => TablePattern.ColumnHeadersProperty;

        public PropertyId RowHeaders => TablePattern.RowHeadersProperty;

        public PropertyId RowOrColumnMajor => TablePattern.RowOrColumnMajorProperty;
    }
}