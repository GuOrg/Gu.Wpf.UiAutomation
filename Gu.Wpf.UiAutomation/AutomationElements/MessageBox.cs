namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class MessageBox : UiElement
    {
        public MessageBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public void Close()
        {
            this.AutomationElement.WindowPattern().Close();
        }
    }
}