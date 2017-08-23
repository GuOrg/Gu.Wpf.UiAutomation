namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IMultipleViewPatternProperties
    {
        PropertyId CurrentView { get; }

        PropertyId SupportedViews { get; }
    }
}