namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Frame : ContentControl
    {
        public Frame(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}