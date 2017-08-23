namespace Gu.Wpf.UiAutomation
{
    public class TitleBar : AutomationElement
    {
        public TitleBar(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public Button MinimizeButton => this.FindButton("Minimize");

        public Button MaximizeButton => this.FindButton("Maximize");

        public Button RestoreButton => this.FindButton("Restore");

        public Button CloseButton => this.FindButton("Close");

        private Button FindButton(string automationId)
        {
            var buttonElement = this.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId(automationId)));
            return buttonElement?.AsButton();
        }
    }
}
