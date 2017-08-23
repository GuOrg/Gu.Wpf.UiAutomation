namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ITablePattern : IPattern
    {
        ITablePatternProperties Properties { get; }

        AutomationProperty<AutomationElement[]> ColumnHeaders { get; }

        AutomationProperty<AutomationElement[]> RowHeaders { get; }

        AutomationProperty<RowOrColumnMajor> RowOrColumnMajor { get; }
    }
}