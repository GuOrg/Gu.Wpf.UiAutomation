namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class DragPatternProperties : IDragPatternProperties
    {
        public PropertyId DropEffect => DragPattern.DropEffectProperty;

        public PropertyId DropEffects => DragPattern.DropEffectsProperty;

        public PropertyId IsGrabbed => DragPattern.IsGrabbedProperty;

        public PropertyId GrabbedItems => DragPattern.GrabbedItemsProperty;
    }
}