namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class ExpandCollapsePattern : ExpandCollapsePatternBase<UIA.IUIAutomationExpandCollapsePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_ExpandCollapsePatternId, "ExpandCollapse", AutomationObjectIds.IsExpandCollapsePatternAvailableProperty);
        public static readonly PropertyId ExpandCollapseStateProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ExpandCollapseExpandCollapseStatePropertyId, "ExpandCollapseState");

        public ExpandCollapsePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationExpandCollapsePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Collapse()
        {
            ComCallWrapper.Call(() => this.NativePattern.Collapse());
        }

        public override void Expand()
        {
            ComCallWrapper.Call(() => this.NativePattern.Expand());
        }
    }
}
