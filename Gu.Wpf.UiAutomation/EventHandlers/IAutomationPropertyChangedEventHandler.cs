namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface IAutomationPropertyChangedEventHandler
    {
        void HandlePropertyChangedEvent(AutomationElement sender, PropertyId propertyId, object newValue);
    }
}
