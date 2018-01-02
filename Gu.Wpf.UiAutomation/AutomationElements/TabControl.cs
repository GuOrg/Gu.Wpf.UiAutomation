namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public class TabControl : Selector<TabItem>
    {
        public TabControl(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public UiElement Content
        {
            get
            {
                var selectedItem = this.SelectedItem ?? throw new InvalidOperationException("TabControl must have a selected item to get Content");
                return selectedItem.Content;
            }
        }
    }
}
