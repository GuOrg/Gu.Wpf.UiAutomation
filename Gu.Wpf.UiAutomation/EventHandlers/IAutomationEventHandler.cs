namespace Gu.Wpf.UiAutomation
{
    public interface IAutomationEventHandler
    {
        void HandleAutomationEvent(AutomationElement sender, EventId eventId);
    }
}
