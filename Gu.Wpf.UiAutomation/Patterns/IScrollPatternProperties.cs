namespace Gu.Wpf.UiAutomation
{
    public interface IScrollPatternProperties
    {
        PropertyId HorizontallyScrollable { get; }

        PropertyId HorizontalScrollPercent { get; }

        PropertyId HorizontalViewSize { get; }

        PropertyId VerticallyScrollable { get; }

        PropertyId VerticalScrollPercent { get; }

        PropertyId VerticalViewSize { get; }
    }
}