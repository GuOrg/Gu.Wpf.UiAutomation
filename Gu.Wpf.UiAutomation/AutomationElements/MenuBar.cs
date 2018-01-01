namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class MenuBar : ContentControl
    {
        public MenuBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}