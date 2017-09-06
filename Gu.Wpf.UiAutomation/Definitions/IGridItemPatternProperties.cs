namespace Gu.Wpf.UiAutomation
{
    public interface IGridItemPatternProperties
    {
        PropertyId Column { get; }

        PropertyId ColumnSpan { get; }

        PropertyId ContainingGrid { get; }

        PropertyId Row { get; }

        PropertyId RowSpan { get; }
    }
}