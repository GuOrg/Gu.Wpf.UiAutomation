namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class GridViewColumnHeader : ColumnHeader
    {
        public GridViewColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}