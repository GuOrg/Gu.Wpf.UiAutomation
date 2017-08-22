using Gu.Wpf.UiAutomation.AutomationElements.PatternElements;

namespace Gu.Wpf.UiAutomation.AutomationElements
{
    public class CheckBox : ToggleAutomationElement
    {
        public CheckBox(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public string Text => Properties.Name.Value;
    }
}
