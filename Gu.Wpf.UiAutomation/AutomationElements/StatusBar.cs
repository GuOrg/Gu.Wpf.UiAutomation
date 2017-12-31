namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class StatusBar : ContentControl
    {
        public StatusBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}