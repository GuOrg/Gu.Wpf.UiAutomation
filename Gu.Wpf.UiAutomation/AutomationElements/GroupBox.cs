namespace Gu.Wpf.UiAutomation
{
    public class GroupBox : Control
    {
        public GroupBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}