namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class CalendarDayButton : SelectionItemControl
    {
        public CalendarDayButton(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text => this.AutomationElement.Text();

        public UiElement Content => this.FindFirstChild();

        public InvokePattern InvokePattern => this.AutomationElement.InvokePattern();

        public void Invoke()
        {
            this.InvokePattern.Invoke();
            _ = Wait.UntilResponsive(this);
        }
    }
}
