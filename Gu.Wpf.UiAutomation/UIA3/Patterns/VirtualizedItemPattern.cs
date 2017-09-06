namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class VirtualizedItemPattern : PatternBase<Interop.UIAutomationClient.IUIAutomationVirtualizedItemPattern>, IVirtualizedItemPattern
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_VirtualizedItemPatternId, "VirtualizedItem", AutomationObjectIds.IsVirtualizedItemPatternAvailableProperty);

        public VirtualizedItemPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationVirtualizedItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public void Realize()
        {
            Com.Call(() => this.NativePattern.Realize());
        }
    }
}
