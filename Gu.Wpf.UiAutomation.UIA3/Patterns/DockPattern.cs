namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class DockPattern : DockPatternBase<UIA.IUIAutomationDockPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_DockPatternId, "Dock", AutomationObjectIds.IsDockPatternAvailableProperty);
        public static readonly PropertyId DockPositionProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_DockDockPositionPropertyId, "DockPosition");

        public DockPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationDockPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetDockPosition(DockPosition dockPos)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetDockPosition((UIA.DockPosition)dockPos));
        }
    }
}
