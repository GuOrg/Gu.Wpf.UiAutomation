namespace Gu.Wpf.UiAutomation.AutomationElements.PatternElements
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns;

    public class InvokeAutomationElement : AutomationElement
    {
        public InvokeAutomationElement(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public IInvokePattern InvokePattern => this.Patterns.Invoke.Pattern;

        public void Invoke()
        {
            this.InvokePattern.Invoke();
        }
    }
}
