namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class ValuePattern : ValuePatternBase<UIA.IUIAutomationValuePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_ValuePatternId, "Value", AutomationObjectIds.IsValuePatternAvailableProperty);
        public static readonly PropertyId IsReadOnlyProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ValueIsReadOnlyPropertyId, "IsReadOnly");
        public static readonly PropertyId ValueProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ValueValuePropertyId, "Value");

        public ValuePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationValuePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetValue(string value)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetValue(value));
        }
    }
}
