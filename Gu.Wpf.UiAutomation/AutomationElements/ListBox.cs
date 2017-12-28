namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class ListBox : Control
    {
        public ListBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Returns the rows which are currently visible to Interop.UIAutomationClient. Might not be the full list (eg. in virtualized lists)!
        /// </summary>
        public virtual IReadOnlyList<ListBoxItem> Items
        {
            get
            {
                return this.FindAll(
                    TreeScope.Children,
                    this.CreateCondition(ControlType.ListItem),
                    x => new ListBoxItem(x));
            }
        }

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

        protected SelectionPattern SelectionPattern => this.AutomationElement.SelectionPattern();

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public ListBoxItem Select(int rowIndex)
        {
            var item = this.FindAt(
                TreeScope.Children,
                this.CreateCondition(ControlType.ListItem),
                rowIndex,
                x => new ListBoxItem(x),
                Retry.Time);
            item.Select();
            return item;
        }

        public ListBoxItem Select(string text)
        {
            var match = this.Items.FirstOrDefault(item => item.Text.Equals(text));
            if (match == null)
            {
                throw new InvalidOperationException($"Did not find an item by text {text}");
            }

            match.Select();
            return match;
        }

        /// <summary>
        /// Add a row to the selection by index.
        /// </summary>
        public ListBoxItem AddToSelection(int rowIndex)
        {
            var item = this.Items.ElementAt(rowIndex);
            item.AddToSelection();
            return item;
        }

        /// <summary>
        /// Remove a row from the selection by index.
        /// </summary>
        public ListBoxItem RemoveFromSelection(int rowIndex)
        {
            var item = this.Items.ElementAt(rowIndex);
            item.RemoveFromSelection();
            return item;
        }
    }
}