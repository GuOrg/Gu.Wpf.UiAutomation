using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Identifiers;

namespace Gu.Wpf.UiAutomation.EventHandlers
{
    public interface IAutomationEventHandler
    {
        void HandleAutomationEvent(AutomationElement sender, EventId eventId);
    }
}
