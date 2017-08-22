namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.UIA2.Identifiers;
    using UIA = System.Windows.Automation;

    public class ScrollItemPattern : PatternBase<UIA.ScrollItemPattern>, IScrollItemPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.ScrollItemPattern.Pattern.Id, "ScrollItem", AutomationObjectIds.IsScrollItemPatternAvailableProperty);

        public ScrollItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.ScrollItemPattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public void ScrollIntoView()
        {
            NativePattern.ScrollIntoView();
        }
    }
}
