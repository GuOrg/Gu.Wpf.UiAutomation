namespace Gu.Wpf.UiAutomation
{
    public class Expander : Control
    {
        public Expander(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}