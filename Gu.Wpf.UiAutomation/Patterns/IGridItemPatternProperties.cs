namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IGridItemPatternProperties
    {
        PropertyId Column { get; }

        PropertyId ColumnSpan { get; }

        PropertyId ContainingGrid { get; }

        PropertyId Row { get; }

        PropertyId RowSpan { get; }
    }
}