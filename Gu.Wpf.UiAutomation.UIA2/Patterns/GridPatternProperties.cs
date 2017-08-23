namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class GridPatternProperties : IGridPatternProperties
    {
        public PropertyId ColumnCount => GridPattern.ColumnCountProperty;

        public PropertyId RowCount => GridPattern.RowCountProperty;
    }
}