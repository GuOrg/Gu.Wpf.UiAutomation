namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class MultiSelector<TItem> : Selector<TItem>
        where TItem : SelectionItemControl
    {
        public MultiSelector(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public IReadOnlyList<TItem> SelectedItems => this.SelectionPattern.Current.GetSelection().Select(x => (TItem)FromAutomationElement(x)).ToArray();

        /// <summary>
        /// Add a row to the selection by index.
        /// </summary>
        public TItem AddToSelection(int rowIndex)
        {
            var item = (TItem)FromAutomationElement(this.AutomationElement.ItemContainerPattern().FindAtIndex(rowIndex));
            item.AddToSelection();
            return item;
        }

        /// <summary>
        /// Remove a row from the selection by index.
        /// </summary>
        public TItem RemoveFromSelection(int rowIndex)
        {
            var item = (TItem)FromAutomationElement(this.AutomationElement.ItemContainerPattern().FindAtIndex(rowIndex));
            item.RemoveFromSelection();
            return item;
        }
    }
}
