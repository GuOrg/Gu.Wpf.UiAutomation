namespace Gu.Wpf.UiAutomation
{
    using System;

    public interface IGridPattern : IPattern
    {
        IGridPatternProperties Properties { get; }

        AutomationProperty<int> ColumnCount { get; }

        AutomationProperty<int> RowCount { get; }

        AutomationElement GetItem(int row, int column);

        T GetItem<T>(int row, int column, Func<BasicAutomationElementBase, T> wrap)
            where T : AutomationElement;
    }
}