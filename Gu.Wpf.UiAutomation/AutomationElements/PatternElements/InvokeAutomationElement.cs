namespace Gu.Wpf.UiAutomation
{
    public class InvokeAutomationElement : Control
    {
        public InvokeAutomationElement(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
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
