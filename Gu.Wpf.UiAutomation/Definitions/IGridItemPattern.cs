namespace Gu.Wpf.UiAutomation
{
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