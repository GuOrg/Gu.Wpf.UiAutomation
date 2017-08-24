namespace Gu.Wpf.UiAutomation
{
    public class Label : Control
    {
        public Label(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}
