namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DataGridColumnHeader : GridHeader
    {
        public DataGridColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}