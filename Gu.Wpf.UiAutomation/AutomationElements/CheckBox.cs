namespace Gu.Wpf.UiAutomation
{
    using Gu.Wpf.UiAutomation.PatternElements;

    public class CheckBox : ToggleAutomationElement
    {
        public CheckBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}
