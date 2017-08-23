namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class ObjectModelPattern : PatternBase<UIA.IUIAutomationObjectModelPattern>, IObjectModelPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_ObjectModelPatternId, "ObjectModel", AutomationObjectIds.IsObjectModelPatternAvailableProperty);

        public ObjectModelPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationObjectModelPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public object GetUnderlyingObjectModel()
        {
            return ComCallWrapper.Call(() => this.NativePattern.GetUnderlyingObjectModel());
        }
    }
}
