namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DragPatternProperties : IDragPatternProperties
    {
        public PropertyId DropEffect => PropertyId.NotSupportedByFramework;

        public PropertyId DropEffects => PropertyId.NotSupportedByFramework;

        public PropertyId IsGrabbed => PropertyId.NotSupportedByFramework;

        public PropertyId GrabbedItems => PropertyId.NotSupportedByFramework;
    }
}