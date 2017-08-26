namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class ValuePattern : ValuePatternBase<Interop.UIAutomationClient.IUIAutomationValuePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_ValuePatternId, "Value", AutomationObjectIds.IsValuePatternAvailableProperty);
        public static readonly PropertyId IsReadOnlyProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_ValueIsReadOnlyPropertyId, "IsReadOnly");
        public static readonly PropertyId ValueProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_ValueValuePropertyId, "Value");

        public ValuePattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationValuePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void SetValue(string value)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetValue(value));
        }
    }
}
