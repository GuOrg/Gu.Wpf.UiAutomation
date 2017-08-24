namespace Gu.Wpf.UiAutomation
{
    using System;
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
        public TabItem[] Items => this.GetTabItems();

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
            var foundTabItemIndex = Array.FindIndex(tabItems, t => t.Properties.Name == text);
            if (foundTabItemIndex < 0)
            {
                throw new Exception($"No TabItem found with text '{text}'");
            }

            var tabItem = tabItems[foundTabItemIndex];
            if (this.SelectedIndex != foundTabItemIndex)
            {
                // It is not the selected one, so select it
                tabItem.Select();
            }

            return tabItem;
        }

        /// <summary>
        /// Gets all the <see cref="TabItem" /> objects for this <see cref="TabControl" />
        /// </summary>
        private TabItem[] GetTabItems()
        {
            return this.FindAll(TreeScope.Children, this.ConditionFactory.ByControlType(ControlType.TabItem))
                .Select(e => e.AsTabItem()).ToArray();
        }

        private int GetIndexOfSelectedTabItem()
        {
            return Array.FindIndex(this.Items, t => t.IsSelected);
        }
    }
}
