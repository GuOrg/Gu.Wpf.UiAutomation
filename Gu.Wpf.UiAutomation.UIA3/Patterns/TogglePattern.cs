namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class TogglePattern : TogglePatternBase<UIA.IUIAutomationTogglePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_TogglePatternId, "Toggle", AutomationObjectIds.IsTogglePatternAvailableProperty);
        public static readonly PropertyId ToggleStateProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ToggleToggleStatePropertyId, "ToggleState");

        public TogglePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationTogglePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Toggle()
        {
            ComCallWrapper.Call(() => this.NativePattern.Toggle());
        }
    }
}
