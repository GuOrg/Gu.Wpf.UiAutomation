namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class InvokeAutomationElement : Control
    {
        public InvokeAutomationElement(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IInvokePattern InvokePattern => this.Patterns.Invoke.Pattern;

        public void Invoke()
        {
            this.InvokePattern.Invoke();
            Wait.UntilResponsive(this);
        }
    }
}
