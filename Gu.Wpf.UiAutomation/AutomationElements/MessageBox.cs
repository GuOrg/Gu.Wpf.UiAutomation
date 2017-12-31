namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class MessageBox : UiElement
    {
        public MessageBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Caption => this.AutomationElement.FindFirst(TreeScope.Children, Condition.TitleBar).Name();

        public string Message => this.AutomationElement.FindFirst(TreeScope.Children, Condition.Label).Name();

        public void Close()
        {
            this.AutomationElement.WindowPattern().Close();
        }
    }
}