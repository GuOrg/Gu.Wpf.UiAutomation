using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Definitions;

namespace Gu.Wpf.UiAutomation.AutomationElements
{
    public class TitleBar : AutomationElement
    {
        public TitleBar(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public Button MinimizeButton => FindButton("Minimize");

        public Button MaximizeButton => FindButton("Maximize");

        public Button RestoreButton => FindButton("Restore");

        public Button CloseButton => FindButton("Close");

        private Button FindButton(string automationId)
        {
            var buttonElement = FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId(automationId)));
            return buttonElement?.AsButton();
        }
    }
}
