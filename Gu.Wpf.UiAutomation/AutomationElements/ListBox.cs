namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class ListBox : Selector
    {
        public ListBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public virtual IReadOnlyList<ListBoxItem> Items => this.ItemContainerPattern.AllItems(x => new ListBoxItem(x)).ToArray();

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public IReadOnlyList<ListBoxItem> SelectedItems => this.SelectionPattern.Current.GetSelection().Select(x => new ListBoxItem(x)).ToArray();

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public ListBoxItem SelectedItem
        {
            get
            {
                var element = this.SelectionPattern.Current.GetSelection().SingleOrDefault();
                return element != null ? new ListBoxItem(element) : null;
            }
        }

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public ListBoxItem Select(int rowIndex)
        {
            var item = new ListBoxItem(this.AutomationElement.ItemContainerPattern().FindAtIndex(rowIndex));
            item.Select();
            return item;
        }

        public ListBoxItem Select(string text)
        {
            var match = new ListBoxItem(this.AutomationElement.ItemContainerPattern().FindByText(text));
            match.Select();
            return match;
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
