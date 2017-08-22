using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Definitions;
using Gu.Wpf.UiAutomation.Patterns;

namespace Gu.Wpf.UiAutomation.AutomationElements.PatternElements
{
    public class ExpandCollapseAutomationElement : AutomationElement
    {
        public ExpandCollapseAutomationElement(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public IExpandCollapsePattern ExpandCollapsePattern => Patterns.ExpandCollapse.Pattern;

        public ExpandCollapseState ExpandCollapseState => ExpandCollapsePattern.ExpandCollapseState;

        public void Expand()
        {
            ExpandCollapsePattern.Expand();
        }

        public void Collapse()
        {
            ExpandCollapsePattern.Expand();
        }
    }
}
