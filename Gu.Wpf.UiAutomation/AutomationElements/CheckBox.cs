namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using Gu.Wpf.UiAutomation.AutomationElements.PatternElements;

    public class CheckBox : ToggleAutomationElement
    {
        public CheckBox(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public string Text => Properties.Name.Value;
    }
}
