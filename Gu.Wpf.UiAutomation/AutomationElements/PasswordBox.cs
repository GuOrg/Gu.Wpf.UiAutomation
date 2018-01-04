namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class PasswordBox : TextBoxBase
    {
        public PasswordBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public void SetValue(string value) => this.AutomationElement.ValuePattern().SetValue(value);
    }
}