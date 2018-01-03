namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Button : InvokeControl
    {
        public Button(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text => this.AutomationElement.Text();

        public UiElement Content => this.FindFirstChild();

        public static Button Create(AutomationElement automationElement)
        {
            if (Conditions.IsMatch(automationElement, Conditions.RepeatButton))
            {
                return new RepeatButton(automationElement);
            }

            return new Button(automationElement);
        }
    }
}
