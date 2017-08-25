namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public interface IDragPattern : IPattern
    {
        IDragPatternProperties Properties { get; }

        IDragPatternEvents Events { get; }

        AutomationProperty<string> DropEffect { get; }

        AutomationProperty<string[]> DropEffects { get; }

        AutomationProperty<bool> IsGrabbed { get; }

        AutomationProperty<IReadOnlyList<AutomationElement>> GrabbedItems { get; }
    }
}