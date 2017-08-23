namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IDragPattern : IPattern
    {
        IDragPatternProperties Properties { get; }

        IDragPatternEvents Events { get; }

        AutomationProperty<string> DropEffect { get; }

        AutomationProperty<string[]> DropEffects { get; }

        AutomationProperty<bool> IsGrabbed { get; }

        AutomationProperty<AutomationElement[]> GrabbedItems { get; }
    }
}