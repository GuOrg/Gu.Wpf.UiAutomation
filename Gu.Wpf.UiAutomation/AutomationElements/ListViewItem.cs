namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ListViewItem : GridRow
    {
        public ListViewItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}