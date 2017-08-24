namespace Gu.Wpf.UiAutomation
{
    public class TextBlock : AutomationElement
    {
        public TextBlock(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}