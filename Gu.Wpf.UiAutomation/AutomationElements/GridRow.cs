namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using System.Linq;
    using Gu.Wpf.UiAutomation.AutomationElements.PatternElements;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns;

    /// <summary>
    /// Row element for grids and tables.
    /// </summary>
    public class GridRow : SelectionItemAutomationElement
    {
        public GridRow(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public GridCell[] Cells
        {
            get
            {
                var cells = this.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem).Not());
                return Enumerable.Select(cells, x => x.AsGridCell()).ToArray();
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