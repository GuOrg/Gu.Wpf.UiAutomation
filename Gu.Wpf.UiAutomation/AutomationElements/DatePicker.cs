namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DatePicker : ContentControl
    {
        public DatePicker(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Value
        {
            get => this.AutomationElement.ValuePattern().Current.Value;
            set => this.AutomationElement.ValuePattern().SetValue(value);
        }
    }
}