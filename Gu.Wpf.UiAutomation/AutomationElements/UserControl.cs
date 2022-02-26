namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class UserControl : Control
    {
        public UserControl(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public UiElement Content => this.FindFirstChild();
    }
}
