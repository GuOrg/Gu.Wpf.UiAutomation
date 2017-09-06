namespace Gu.Wpf.UiAutomation
{
    public interface IDragPatternProperties
    {
        PropertyId DropEffect { get; }

        PropertyId DropEffects { get; }

        PropertyId IsGrabbed { get; }

        PropertyId GrabbedItems { get; }
    }
}