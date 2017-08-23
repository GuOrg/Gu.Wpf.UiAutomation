namespace Gu.Wpf.UiAutomation
{
    public class ExpandCollapseAutomationElement : AutomationElement
    {
        public ExpandCollapseAutomationElement(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public IExpandCollapsePattern ExpandCollapsePattern => this.Patterns.ExpandCollapse.Pattern;

        public ExpandCollapseState ExpandCollapseState => this.ExpandCollapsePattern.ExpandCollapseState;

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
