namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TabControl : AutomationElement
    {
        public TabControl(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// The currently selected <see cref="TabItem" />
        /// </summary>
        public TabItem SelectedItem
        {
            get { return this.Items.FirstOrDefault(t => t.IsSelected); }
        }

        /// <summary>
        /// The index of the currently selected <see cref="TabItem" />
        /// </summary>
        public int SelectedIndex
        {
            get => this.GetIndexOfSelectedTabItem();
            set => this.Select(value);
        }

        /// <summary>
        /// All <see cref="TabItem" /> objects from this <see cref="TabControl" />
        /// </summary>
        public IReadOnlyList<TabItem> Items => this.FindAllChildren(this.ConditionFactory.ByControlType(ControlType.TabItem), x => new TabItem(x));

        public AutomationElement Content
        {
            get
            {
                var selectedItem = this.SelectedItem ?? throw new InvalidOperationException("TabControl must have a selected item to get Content");
                return selectedItem.Content;
            }
        }

        /// <summary>
        /// Selects a <see cref="TabItem" /> by index
        /// </summary>
        public TabItem Select(int index)
        {
            var tabItem = this.Items[index];
            tabItem.Select();
            return tabItem;
        }

        /// <summary>
        /// Selects a <see cref="TabItem" /> by a give text (name property)
        /// </summary>
        public TabItem Select(string text)
        {
            var tabItems = this.Items;
            var tabItem = tabItems.FirstOrDefault(t => t.Properties.Name.ValueOrDefault("MISSING_VALUE") == text);
            if (tabItem == null)
            {
                throw new Exception($"No TabItem found with text '{text}'");
            }

            if (!tabItem.IsSelected)
            {
                // It is not the selected one, so select it
                tabItem.Select();
            }

            return tabItem;
        }

        private int GetIndexOfSelectedTabItem()
        {
            var items = this.Items;
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].IsSelected)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
