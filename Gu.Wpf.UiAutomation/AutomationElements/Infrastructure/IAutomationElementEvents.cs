using Gu.Wpf.UiAutomation.Identifiers;

namespace Gu.Wpf.UiAutomation.AutomationElements.Infrastructure
{
    public interface IAutomationElementEvents
    {
        EventId AsyncContentLoadedEvent { get; }
        EventId FocusChangedEvent { get; }
        EventId PropertyChangedEvent { get; }
        EventId HostedFragmentRootsInvalidatedEvent { get; }
        EventId LayoutInvalidatedEvent { get; }
        EventId LiveRegionChangedEvent { get; }
        EventId MenuClosedEvent { get; }
        EventId MenuModeEndEvent { get; }
        EventId MenuModeStartEvent { get; }
        EventId MenuOpenedEvent { get; }
        EventId StructureChangedEvent { get; }
        EventId SystemAlertEvent { get; }
        EventId ToolTipClosedEvent { get; }
        EventId ToolTipOpenedEvent { get; }
    }
}
