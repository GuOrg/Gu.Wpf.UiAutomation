namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class RangeValuePattern : RangeValuePatternBase<UIA.IUIAutomationRangeValuePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_RangeValuePatternId, "RangeValue", AutomationObjectIds.IsRangeValuePatternAvailableProperty);
        public static readonly PropertyId IsReadOnlyProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_RangeValueIsReadOnlyPropertyId, "IsReadOnly");
        public static readonly PropertyId LargeChangeProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_RangeValueLargeChangePropertyId, "LargeChange");
        public static readonly PropertyId MaximumProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_RangeValueMaximumPropertyId, "Maximum");
        public static readonly PropertyId MinimumProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_RangeValueMinimumPropertyId, "Minimum");
        public static readonly PropertyId SmallChangeProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_RangeValueSmallChangePropertyId, "SmallChange");
        public static readonly PropertyId ValueProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_RangeValueValuePropertyId, "Value");

        public RangeValuePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationRangeValuePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetValue(double val)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetValue(val));
        }
    }

    public class RangeValuePatternProperties : IRangeValuePatternProperties
    {
        public PropertyId IsReadOnly => RangeValuePattern.IsReadOnlyProperty;

        public PropertyId LargeChange => RangeValuePattern.LargeChangeProperty;

        public PropertyId Maximum => RangeValuePattern.MaximumProperty;

        public PropertyId Minimum => RangeValuePattern.MinimumProperty;

        public PropertyId SmallChange => RangeValuePattern.SmallChangeProperty;

        public PropertyId Value => RangeValuePattern.ValueProperty;
    }
}
