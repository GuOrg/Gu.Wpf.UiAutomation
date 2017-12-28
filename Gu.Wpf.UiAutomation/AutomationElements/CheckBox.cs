namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class CheckBox : ToggleButton
    {
        public CheckBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}
