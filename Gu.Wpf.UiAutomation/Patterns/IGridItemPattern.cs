namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IGridItemPattern : IPattern
    {
        IGridItemPatternProperties Properties { get; }

        AutomationProperty<int> Column { get; }

        AutomationProperty<int> ColumnSpan { get; }

        AutomationProperty<AutomationElement> ContainingGrid { get; }

        AutomationProperty<int> Row { get; }

        AutomationProperty<int> RowSpan { get; }
    }
}