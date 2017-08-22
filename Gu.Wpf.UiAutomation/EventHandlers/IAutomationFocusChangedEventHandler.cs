using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

namespace Gu.Wpf.UiAutomation.EventHandlers
{
    public interface IAutomationFocusChangedEventHandler
    {
        void HandleFocusChangedEvent(AutomationElement sender);
    }
}
