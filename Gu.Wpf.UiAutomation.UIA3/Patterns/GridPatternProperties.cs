namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class GridPatternProperties : IGridPatternProperties
    {
        public PropertyId ColumnCount => GridPattern.ColumnCountProperty;

        public PropertyId RowCount => GridPattern.RowCountProperty;
    }
}