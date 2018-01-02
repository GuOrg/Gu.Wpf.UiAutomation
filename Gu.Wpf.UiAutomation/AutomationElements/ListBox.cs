namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ListBox : Selector<ListBoxItem, ListBoxItem>
    {
        public ListBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Add a row to the selection by index.
        /// </summary>
        public ListBoxItem AddToSelection(int rowIndex)
        {
            var item = new ListBoxItem(this.AutomationElement.ItemContainerPattern().FindAtIndex(rowIndex));
            item.AddToSelection();
            return item;
        }

        /// <summary>
        /// Remove a row from the selection by index.
        /// </summary>
        public ListBoxItem RemoveFromSelection(int rowIndex)
        {
            var item = new ListBoxItem(this.AutomationElement.ItemContainerPattern().FindAtIndex(rowIndex));
            item.RemoveFromSelection();
            return item;
        }
    }
}
