namespace Gu.Wpf.UiAutomation
{
    public class CheckBox : ToggleAutomationElement
    {
        public CheckBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}
