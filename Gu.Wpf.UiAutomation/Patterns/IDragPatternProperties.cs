namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IDragPatternProperties
    {
        PropertyId DropEffect { get; }

        PropertyId DropEffects { get; }

        PropertyId IsGrabbed { get; }

        PropertyId GrabbedItems { get; }
    }
}