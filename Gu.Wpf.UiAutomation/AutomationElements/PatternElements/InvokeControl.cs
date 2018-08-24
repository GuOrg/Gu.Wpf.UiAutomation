namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class InvokeControl : Control
    {
        public InvokeControl(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public InvokePattern InvokePattern => this.AutomationElement.InvokePattern();

        public void Invoke()
        {
            this.InvokePattern.Invoke();
            _ = Wait.UntilResponsive(this);
        }
    }
}
