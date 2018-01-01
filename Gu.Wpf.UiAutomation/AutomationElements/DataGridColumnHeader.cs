namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DataGridColumnHeader : ColumnHeader
    {
        public DataGridColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}