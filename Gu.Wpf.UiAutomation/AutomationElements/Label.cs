using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

namespace Gu.Wpf.UiAutomation.AutomationElements
{
    public class Label : AutomationElement
    {
        public Label(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public string Text => Properties.Name.Value;
    }
}
