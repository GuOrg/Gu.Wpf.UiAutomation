namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class ScrollItemPattern : PatternBase<Interop.UIAutomationClient.IUIAutomationScrollItemPattern>, IScrollItemPattern
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_ScrollItemPatternId, "ScrollItem", AutomationObjectIds.IsScrollItemPatternAvailableProperty);

        public ScrollItemPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationScrollItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public void ScrollIntoView()
        {
            ComCallWrapper.Call(() => this.NativePattern.ScrollIntoView());
        }
    }
}
