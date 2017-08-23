namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IAutomationEventHandler
    {
        void HandleAutomationEvent(AutomationElement sender, EventId eventId);
    }
}
