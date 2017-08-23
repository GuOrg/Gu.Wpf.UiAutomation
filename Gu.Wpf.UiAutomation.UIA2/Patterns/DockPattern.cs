namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA2.Identifiers;
    using UIA = System.Windows.Automation;

    public class DockPattern : DockPatternBase<UIA.DockPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.DockPattern.Pattern.Id, "Dock", AutomationObjectIds.IsDockPatternAvailableProperty);
        public static readonly PropertyId DockPositionProperty = PropertyId.Register(AutomationType.UIA2, UIA.DockPattern.DockPositionProperty.Id, "DockPosition");

        public DockPattern(BasicAutomationElementBase basicAutomationElement, UIA.DockPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetDockPosition(DockPosition dockPos)
        {
            this.NativePattern.SetDockPosition((UIA.DockPosition)dockPos);
        }
    }
}
