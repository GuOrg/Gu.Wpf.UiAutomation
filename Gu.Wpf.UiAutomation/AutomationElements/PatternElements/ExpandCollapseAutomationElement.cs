namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ExpandCollapseAutomationElement : Control
    {
        public ExpandCollapseAutomationElement(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ExpandCollapsePattern ExpandCollapsePattern => this.AutomationElement.ExpandCollapsePattern();

        public ExpandCollapseState ExpandCollapseState => this.ExpandCollapsePattern.Current.ExpandCollapseState;

        public void Expand()
        {
            this.ExpandCollapsePattern.Expand();
        }

        public void Collapse()
        {
            this.ExpandCollapsePattern.Expand();
        }
    }
}
