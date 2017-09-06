namespace Gu.Wpf.UiAutomation
{
    public class ExpandCollapseAutomationElement : Control
    {
        public ExpandCollapseAutomationElement(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
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
