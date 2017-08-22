namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

    public class Label : AutomationElement
    {
        public Label(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}
