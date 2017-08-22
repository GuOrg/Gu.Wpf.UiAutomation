namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

    public interface IAutomationFocusChangedEventHandler
    {
        void HandleFocusChangedEvent(AutomationElement sender);
    }
}
