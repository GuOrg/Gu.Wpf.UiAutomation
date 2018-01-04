namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Dialog : UiElement
    {
        public Dialog(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TransformPattern TransformPattern => this.AutomationElement.TransformPattern();

        public WindowPattern WindowPattern => this.AutomationElement.WindowPattern();

        public void Close()
        {
            this.AutomationElement.WindowPattern().Close();
        }
    }
}