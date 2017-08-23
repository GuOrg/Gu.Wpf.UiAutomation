namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class ScrollPatternProperties : IScrollPatternProperties
    {
        public PropertyId HorizontallyScrollable => ScrollPattern.HorizontallyScrollableProperty;

        public PropertyId HorizontalScrollPercent => ScrollPattern.HorizontalScrollPercentProperty;

        public PropertyId HorizontalViewSize => ScrollPattern.HorizontalViewSizeProperty;

        public PropertyId VerticallyScrollable => ScrollPattern.VerticallyScrollableProperty;

        public PropertyId VerticalScrollPercent => ScrollPattern.VerticalScrollPercentProperty;

        public PropertyId VerticalViewSize => ScrollPattern.VerticalViewSizeProperty;
    }
}