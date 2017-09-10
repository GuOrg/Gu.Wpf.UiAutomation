namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Row element for grids and tables.
    /// </summary>
    public class GridRow : SelectionItemAutomationElement
    {
        public GridRow(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public IReadOnlyList<GridCell> Cells
        {
            get
            {
                return this.FindAllChildren()
                           .Where(x => x.Patterns.TableItem.IsSupported)
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

                if (this.Patterns.VirtualizedItem.TryGetPattern(out var pattern))
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

        protected IScrollItemPattern ScrollItemPattern => this.Patterns.ScrollItem.Pattern;

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