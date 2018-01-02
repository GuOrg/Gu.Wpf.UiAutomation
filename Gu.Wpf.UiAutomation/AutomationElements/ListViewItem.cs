namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class ListViewItem : SelectionItemControl
    {
        public ListViewItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ListView ContainingListView => (ListView)FromAutomationElement(this.SelectionItemPattern.Current.SelectionContainer);

        public IReadOnlyList<GridViewCell> Cells => this.AutomationElement.FindAllChildren(Condition.GridViewCell)
                                                        .OfType<AutomationElement>()
                                                        .Select(x => GridViewCell.Create(FromAutomationElement(x)))
                                                        .ToArray();

        public ScrollItemPattern ScrollItemPattern => this.AutomationElement.ScrollItemPattern();

        /// <summary>
        /// Find a cell by a given text.
        /// </summary>
        public GridViewCell FindCellByText(string textToFind)
        {
            return this.Cells.FirstOrDefault(cell => Equals(cell.Text, textToFind));
        }

        public ListViewItem ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}
