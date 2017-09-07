namespace Gu.Wpf.UiAutomation
{
    public class UserControl : Control
    {
        public UserControl(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public AutomationElement Content => this.FindFirstChild();
    }
}