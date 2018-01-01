namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ToolTip : ContentControl
    {
        public ToolTip(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}