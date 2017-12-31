namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ColumnHeader : GridHeader
    {
        public ColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}