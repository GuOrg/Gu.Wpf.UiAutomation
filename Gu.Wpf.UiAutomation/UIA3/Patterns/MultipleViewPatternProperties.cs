namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class MultipleViewPatternProperties : IMultipleViewPatternProperties
    {
        public PropertyId CurrentView => MultipleViewPattern.CurrentViewProperty;

        public PropertyId SupportedViews => MultipleViewPattern.SupportedViewsProperty;
    }
}