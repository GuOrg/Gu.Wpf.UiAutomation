namespace Gu.Wpf.UiAutomation.UIA3.EventHandlers
{
    using System;

    public class UIA3PropertyChangedEventHandler : PropertyChangedEventHandlerBase, Interop.UIAutomationClient.IUIAutomationPropertyChangedEventHandler
    {
        public UIA3PropertyChangedEventHandler(AutomationBase automation, Action<AutomationElement, PropertyId, object> callAction)
            : base(automation, callAction)
        {
        }

        public void HandlePropertyChangedEvent(Interop.UIAutomationClient.IUIAutomationElement sender, int propertyId, object newValue)
        {
            var basicAutomationElement = new UIA3BasicAutomationElement((UIA3Automation)this.Automation, sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            var property = PropertyId.Find(propertyId);
            this.HandlePropertyChangedEvent(senderElement, property, newValue);
        }
    }
}
