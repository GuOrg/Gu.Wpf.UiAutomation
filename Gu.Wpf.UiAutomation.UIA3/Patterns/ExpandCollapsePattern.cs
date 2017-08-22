namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class ExpandCollapsePattern : ExpandCollapsePatternBase<UIA.IUIAutomationExpandCollapsePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_ExpandCollapsePatternId, "ExpandCollapse", AutomationObjectIds.IsExpandCollapsePatternAvailableProperty);
        public static readonly PropertyId ExpandCollapseStateProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_ExpandCollapseExpandCollapseStatePropertyId, "ExpandCollapseState");

        public ExpandCollapsePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationExpandCollapsePattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Collapse()
        {
            ComCallWrapper.Call(() => NativePattern.Collapse());
        }

        public override void Expand()
        {
            ComCallWrapper.Call(() => NativePattern.Expand());
        }
    }

    public class ExpandCollapsePatternProperties : IExpandCollapsePatternProperties
    {
        public PropertyId ExpandCollapseState => ExpandCollapsePattern.ExpandCollapseStateProperty;
    }
}
