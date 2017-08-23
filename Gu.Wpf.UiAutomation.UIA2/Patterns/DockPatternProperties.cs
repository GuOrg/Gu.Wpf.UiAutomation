namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DockPatternProperties : IDockPatternProperties
    {
        public PropertyId DockPosition => DockPattern.DockPositionProperty;
    }
}