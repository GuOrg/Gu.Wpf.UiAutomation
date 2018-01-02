namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class DataGridRow : SelectionItemControl
    {
        public DataGridRow(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public DataGridRowHeader Header
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, Condition.HeaderItem, x => (DataGridRowHeader)FromAutomationElement(x), Retry.Time, out var header))
                {
                    return header;
                }

                if (this.AutomationElement.TryGetVirtualizedItemPattern(out var pattern))
                {
                    pattern.Realize();
                    if (this.TryFindFirst(TreeScope.Children, Condition.HeaderItem, x => (DataGridRowHeader)FromAutomationElement(x), Retry.Time, out header))
                    {
                        return header;
                    }
                }

                return null;
            }
        }

        public DataGrid ContainingDataGrid => (DataGrid)FromAutomationElement(this.SelectionItemPattern.Current.SelectionContainer);

        public IReadOnlyList<DataGridCell> Cells => this.AutomationElement.FindAllChildren(Condition.IsTableItemPatternAvailable)
                                                        .OfType<AutomationElement>()
                                                        .Select(x => new DataGridCell(x))
                                                        .ToArray();

        public ScrollItemPattern ScrollItemPattern => this.AutomationElement.ScrollItemPattern();

        /// <summary>
        /// Find a cell by a given text.
        /// </summary>
        public DataGridCell FindCellByText(string textToFind)
        {
            return this.Cells.FirstOrDefault(cell => cell.Value.Equals(textToFind));
        }

        public DataGridRow ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}
