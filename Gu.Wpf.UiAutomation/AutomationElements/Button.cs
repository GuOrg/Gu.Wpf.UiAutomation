namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Button : InvokeAutomationElement
    {
        public Button(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string Text
        {
            get
            {
                var children = this.FindAllChildren();
                if (children.Count == 1 &&
                    children[0].ControlType == ControlType.Text)
                {
                    return children[0].Properties.Name.Value;
                }

                return this.Properties.Name.Value;
            }
        }

        public UiElement Content => this.FindFirstChild();
    }
}
