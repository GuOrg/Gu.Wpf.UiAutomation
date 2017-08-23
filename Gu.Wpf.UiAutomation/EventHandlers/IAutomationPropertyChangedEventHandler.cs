namespace Gu.Wpf.UiAutomation
{
    public interface IAutomationPropertyChangedEventHandler
    {
        void HandlePropertyChangedEvent(AutomationElement sender, PropertyId propertyId, object newValue);
    }
}
