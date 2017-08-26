namespace Gu.Wpf.UiAutomation.UIA3.EventHandlers
{
    using System;

    public class UIA3BasicEventHandler : BasicEventHandlerBase, Interop.UIAutomationClient.IUIAutomationEventHandler
    {
        public UIA3BasicEventHandler(AutomationBase automation, Action<AutomationElement, EventId> callAction)
            : base(automation, callAction)
        {
        }

        public void HandleAutomationEvent(Interop.UIAutomationClient.IUIAutomationElement sender, int eventId)
        {
            var basicAutomationElement = new UIA3BasicAutomationElement((UIA3Automation)this.Automation, sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            var @event = EventId.Find(eventId);
            this.HandleAutomationEvent(senderElement, @event);
        }
    }
}
