namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ListBox : MultiSelector<ListBoxItem>
    {
        public ListBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}
