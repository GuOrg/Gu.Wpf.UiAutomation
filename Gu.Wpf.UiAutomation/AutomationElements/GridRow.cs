namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    /// <summary>
    /// Row element for grids and tables.
    /// </summary>
    public class GridRow : SelectionItemControl
    {
        private static readonly PropertyCondition TableItemCondition = new PropertyCondition(AutomationElement.IsTableItemPatternAvailableProperty, true);

        public GridRow(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IReadOnlyList<GridCell> Cells => this.AutomationElement.FindAllChildren(TableItemCondition)
                                                    .OfType<AutomationElement>()
                                                    .Select(x => new GridCell(x))
                                                    .ToArray();

        public RowHeader Header
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, Condition.HeaderItem, x => (RowHeader)FromAutomationElement(x), Retry.Time, out var header))
                {
                    return header;
                }

                if (this.AutomationElement.TryGetVirtualizedItemPattern(out var pattern))
                {
                    pattern.Realize();
                    if (this.TryFindFirst(TreeScope.Children, Condition.HeaderItem, x => (RowHeader)FromAutomationElement(x), Retry.Time, out header))
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
