namespace Gu.Wpf.UiAutomation
{
    public interface IMultipleViewPatternProperties
    {
        PropertyId CurrentView { get; }

        PropertyId SupportedViews { get; }
    }
}