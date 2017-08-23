namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class GridItemPatternProperties : IGridItemPatternProperties
    {
        public PropertyId Column => GridItemPattern.ColumnProperty;

        public PropertyId ColumnSpan => GridItemPattern.ColumnSpanProperty;

        public PropertyId ContainingGrid => GridItemPattern.ContainingGridProperty;

        public PropertyId Row => GridItemPattern.RowProperty;

        public PropertyId RowSpan => GridItemPattern.RowSpanProperty;
    }
}