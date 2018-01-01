namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DataGridRow : GridRow
    {
        public DataGridRow(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}