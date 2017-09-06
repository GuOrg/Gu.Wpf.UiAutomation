namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public interface ITablePattern : IPattern
    {
        ITablePatternProperties Properties { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> ColumnHeaders { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> RowHeaders { get; }

        AutomationProperty<RowOrColumnMajor> RowOrColumnMajor { get; }
    }
}