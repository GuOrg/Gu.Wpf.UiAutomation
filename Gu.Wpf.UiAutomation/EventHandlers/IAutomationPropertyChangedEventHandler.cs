using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Identifiers;

namespace Gu.Wpf.UiAutomation.EventHandlers
{
    public interface IAutomationPropertyChangedEventHandler
    {
        void HandlePropertyChangedEvent(AutomationElement sender, PropertyId propertyId, object newValue);
    }
}
