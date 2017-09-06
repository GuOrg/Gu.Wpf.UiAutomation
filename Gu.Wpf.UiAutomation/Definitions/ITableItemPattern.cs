namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public interface ITableItemPattern : IPattern
    {
        ITableItemPatternProperties Properties { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> ColumnHeaderItems { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> RowHeaderItems { get; }
    }
}