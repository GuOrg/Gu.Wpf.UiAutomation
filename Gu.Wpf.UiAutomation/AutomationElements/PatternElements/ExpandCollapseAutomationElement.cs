namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ExpandCollapseAutomationElement : Control
    {
        public ExpandCollapseAutomationElement(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IExpandCollapsePattern ExpandCollapsePattern => this.Patterns.ExpandCollapse.Pattern;

        public ExpandCollapseState ExpandCollapseState => this.ExpandCollapsePattern.ExpandCollapseState.Value;

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
