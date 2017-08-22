namespace Gu.Wpf.UiAutomation.AutomationElements.PatternElements
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns;

    public class ExpandCollapseAutomationElement : AutomationElement
    {
        public ExpandCollapseAutomationElement(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
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
