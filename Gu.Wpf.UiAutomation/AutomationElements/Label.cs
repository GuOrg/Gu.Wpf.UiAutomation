namespace Gu.Wpf.UiAutomation
{
    public class Label : AutomationElement
    {
        public Label(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}
