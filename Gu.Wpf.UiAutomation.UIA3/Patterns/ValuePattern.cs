namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class ValuePattern : ValuePatternBase<UIA.IUIAutomationValuePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_ValuePatternId, "Value", AutomationObjectIds.IsValuePatternAvailableProperty);
        public static readonly PropertyId IsReadOnlyProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_ValueIsReadOnlyPropertyId, "IsReadOnly");
        public static readonly PropertyId ValueProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_ValueValuePropertyId, "Value");

        public ValuePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationValuePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetValue(string value)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetValue(value));
        }
    }

    public class ValuePatternProperties : IValuePatternProperties
    {
        public PropertyId IsReadOnly => ValuePattern.IsReadOnlyProperty;

        public PropertyId Value => ValuePattern.ValueProperty;
    }
}
