namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class MessageBox : Dialog
    {
        public MessageBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Caption => this.AutomationElement.FindFirst(TreeScope.Children, Conditions.TitleBar).Name();

        public string Message => this.AutomationElement.FindFirst(TreeScope.Children, Conditions.Label).Name();
    }
}
