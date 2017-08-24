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
                return this.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem).Not())
                           .Select(x => x.AsGridCell())
                           .ToArray();
            }
        }

        public GridHeaderItem Header
        {
            get
            {
                var headerItem = this.FindFirstChild(this.ConditionFactory.ByControlType(ControlType.HeaderItem));
                return headerItem?.AsGridHeaderItem();
            }
        }

        protected IScrollItemPattern ScrollItemPattern => this.Patterns.ScrollItem.Pattern;

        /// <summary>
        /// Find a cell by a given text.
        /// </summary>
        public GridCell FindCellByText(string textToFind)
        {
            return Enumerable.FirstOrDefault(this.Cells, cell => cell.Value.Equals(textToFind));
        }

        public GridRow ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}