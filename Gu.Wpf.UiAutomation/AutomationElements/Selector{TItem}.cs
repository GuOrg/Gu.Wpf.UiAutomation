namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class Selector<TItem> : Selector
        where TItem : SelectionItemControl
    {
        public Selector(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public virtual IReadOnlyList<TItem> Items => this.ItemContainerPattern.AllItems(x => (TItem)FromAutomationElement(x)).ToArray();

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public TItem SelectedItem
        {
            get
            {
                var element = this.SelectionPattern.Current.GetSelection().SingleOrDefault();
                return element != null ? (TItem)FromAutomationElement(element) : null;
            }
        }

        /// <summary>
        /// The index of the currently selected <see cref="TabItem" />.
        /// </summary>
        public int SelectedIndex
        {
            get => this.GetIndexOfSelectedTabItem();
            set => this.Select(value);
        }

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public TItem Select(int rowIndex)
        {
            var item = (TItem)FromAutomationElement(this.AutomationElement.ItemContainerPattern().FindAtIndex(rowIndex));
            if (item.AutomationElement.TryGetVirtualizedItemPattern(out var pattern))
            {
                pattern.Realize();
            }

            item.Select();
            return item;
        }

        public TItem Select(string text)
        {
            var item = (TItem)FromAutomationElement(this.AutomationElement.ItemContainerPattern().FindByText(text));
            if (item.AutomationElement.TryGetVirtualizedItemPattern(out var pattern))
            {
                pattern.Realize();
            }

            item.Select();
            return item;
        }

        private int GetIndexOfSelectedTabItem()
        {
            var selection = this.SelectionPattern.Current.GetSelection();
            if (selection.Length != 1)
            {
                return -1;
            }

            var selectedItem = selection[0];
            var index = 0;
            foreach (var item in this.ItemContainerPattern.AllItems())
            {
                if (Equals(item, selectedItem))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
