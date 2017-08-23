namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
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