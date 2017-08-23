namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class WindowPatternProperties : IWindowPatternProperties
    {
        public PropertyId CanMaximize => WindowPattern.CanMaximizeProperty;

        public PropertyId CanMinimize => WindowPattern.CanMinimizeProperty;

        public PropertyId IsModal => WindowPattern.IsModalProperty;

        public PropertyId IsTopmost => WindowPattern.IsTopmostProperty;

        public PropertyId WindowInteractionState => WindowPattern.WindowInteractionStateProperty;

        public PropertyId WindowVisualState => WindowPattern.WindowVisualStateProperty;
    }
}