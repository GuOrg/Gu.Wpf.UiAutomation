namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Label : Control
    {
        public Label(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }
}
