namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class VirtualizedItemPattern : PatternBase<UIA.IUIAutomationVirtualizedItemPattern>, IVirtualizedItemPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_VirtualizedItemPatternId, "VirtualizedItem", AutomationObjectIds.IsVirtualizedItemPatternAvailableProperty);

        public VirtualizedItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationVirtualizedItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public void Realize()
        {
            ComCallWrapper.Call(() => this.NativePattern.Realize());
        }
    }
}
