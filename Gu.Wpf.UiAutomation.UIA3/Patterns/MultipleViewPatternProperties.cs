namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class MultipleViewPatternProperties : IMultipleViewPatternProperties
    {
        public PropertyId CurrentView => MultipleViewPattern.CurrentViewProperty;

        public PropertyId SupportedViews => MultipleViewPattern.SupportedViewsProperty;
    }
}