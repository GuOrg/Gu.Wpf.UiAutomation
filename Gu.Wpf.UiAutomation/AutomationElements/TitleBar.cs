namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class TitleBar : UiElement
    {
        public TitleBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Button MinimizeButton => this.FindButton("Minimize");

        public Button MaximizeButton => this.FindButton("Maximize");

        public Button RestoreButton => this.FindButton("Restore");

        public Button CloseButton => this.FindButton("Close");
    }
}
