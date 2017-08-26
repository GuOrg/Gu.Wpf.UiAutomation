namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class RangeValuePattern : RangeValuePatternBase<Interop.UIAutomationClient.IUIAutomationRangeValuePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_RangeValuePatternId, "RangeValue", AutomationObjectIds.IsRangeValuePatternAvailableProperty);
        public static readonly PropertyId IsReadOnlyProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_RangeValueIsReadOnlyPropertyId, "IsReadOnly");
        public static readonly PropertyId LargeChangeProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_RangeValueLargeChangePropertyId, "LargeChange");
        public static readonly PropertyId MaximumProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_RangeValueMaximumPropertyId, "Maximum");
        public static readonly PropertyId MinimumProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_RangeValueMinimumPropertyId, "Minimum");
        public static readonly PropertyId SmallChangeProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_RangeValueSmallChangePropertyId, "SmallChange");
        public static readonly PropertyId ValueProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_RangeValueValuePropertyId, "Value");

        public RangeValuePattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationRangeValuePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetValue(double val)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetValue(val));
        }
    }
}
