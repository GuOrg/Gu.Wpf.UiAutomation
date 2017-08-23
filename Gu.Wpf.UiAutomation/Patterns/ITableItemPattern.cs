namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ITableItemPattern : IPattern
    {
        ITableItemPatternProperties Properties { get; }

        AutomationProperty<AutomationElement[]> ColumnHeaderItems { get; }

        AutomationProperty<AutomationElement[]> RowHeaderItems { get; }
    }
}