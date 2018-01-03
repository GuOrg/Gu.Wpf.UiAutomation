namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Button : InvokeControl
    {
        public Button(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text
        {
            get
            {
                var children = this.AutomationElement.FindAllChildren(Condition.TrueCondition);
                if (children.Count == 1 &&
                    children[0].Current.ControlType.Id == ControlType.Text.Id)
                {
                    return children[0].Current.Name;
                }

                return this.Name;
            }
        }

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
