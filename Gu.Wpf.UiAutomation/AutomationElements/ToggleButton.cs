namespace Gu.Wpf.UiAutomation
{
    public class ToggleButton : ToggleAutomationElement
    {
        public ToggleButton(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}