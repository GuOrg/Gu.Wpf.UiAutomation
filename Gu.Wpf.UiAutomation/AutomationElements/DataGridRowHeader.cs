namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DataGridRowHeader : GridHeader
    {
        public DataGridRowHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}