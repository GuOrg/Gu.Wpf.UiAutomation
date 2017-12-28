namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    /// <summary>
    /// Row element for grids and tables.
    /// </summary>
    public class GridRow : SelectionItemAutomationElement
    {
        public GridRow(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IReadOnlyList<GridCell> Cells
        {
            get
            {
                return this.FindAllChildren()
                           .Where(x => x.AutomationElement.TryGetCurrentPattern(TableItemPattern.Pattern, out _))
                           .Select(x => x.AsGridCell())
                           .ToArray();
            }
        }

        public RowHeader Header
        {
            get
            {
                if (this.TryFindFirst(
                    TreeScope.Children,
                    this.CreateCondition(ControlType.HeaderItem),
                    x => new RowHeader(x),
                    Retry.Time,
                    out var header))
                {
                    return header;
                }

                if (this.AutomationElement.TryGetVirtualizedItemPattern(out var pattern))
                {
                    pattern.Realize();
                    if (this.TryFindFirst(
                        TreeScope.Children,
                        this.CreateCondition(ControlType.HeaderItem),
                        x => new RowHeader(x),
                        Retry.Time,
                        out header))
                    {
                        return header;
                    }
                }

                return null;
            }
        }

        protected ScrollItemPattern ScrollItemPattern => this.AutomationElement.ScrollItemPattern();

        /// <summary>
        /// Find a cell by a given text.
        /// </summary>
        public GridCell FindCellByText(string textToFind)
        {
            return this.Cells.FirstOrDefault(cell => cell.Value.Equals(textToFind));
        }

        public GridRow ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}