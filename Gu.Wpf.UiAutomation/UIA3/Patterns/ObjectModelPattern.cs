namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class ObjectModelPattern : PatternBase<Interop.UIAutomationClient.IUIAutomationObjectModelPattern>, IObjectModelPattern
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_ObjectModelPatternId, "ObjectModel", AutomationObjectIds.IsObjectModelPatternAvailableProperty);

        public ObjectModelPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationObjectModelPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public object GetUnderlyingObjectModel()
        {
            return Com.Call(() => this.NativePattern.GetUnderlyingObjectModel());
        }
    }
}
