namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA2.Identifiers;
    using UIA = System.Windows.Automation;

    public class ExpandCollapsePattern : ExpandCollapsePatternBase<UIA.ExpandCollapsePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.ExpandCollapsePattern.Pattern.Id, "ExpandCollapse", AutomationObjectIds.IsExpandCollapsePatternAvailableProperty);
        public static readonly PropertyId ExpandCollapseStateProperty = PropertyId.Register(AutomationType.UIA2, UIA.ExpandCollapsePattern.ExpandCollapseStateProperty.Id, "ExpandCollapseState");

        public ExpandCollapsePattern(BasicAutomationElementBase basicAutomationElement, UIA.ExpandCollapsePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Collapse()
        {
            this.NativePattern.Collapse();
        }

        public override void Expand()
        {
            this.NativePattern.Expand();
        }
    }
}
