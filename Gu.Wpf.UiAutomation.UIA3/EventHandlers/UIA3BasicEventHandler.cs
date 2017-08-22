namespace Gu.Wpf.UiAutomation.UIA3.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using Gu.Wpf.UiAutomation.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class UIA3BasicEventHandler : BasicEventHandlerBase, UIA.IUIAutomationEventHandler
    {
        public UIA3BasicEventHandler(AutomationBase automation, Action<AutomationElement, EventId> callAction)
            : base(automation, callAction)
        {
        }

        public void HandleAutomationEvent(UIA.IUIAutomationElement sender, int eventId)
        {
            var basicAutomationElement = new UIA3BasicAutomationElement((UIA3Automation)this.Automation, sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            var @event = EventId.Find(AutomationType.UIA3, eventId);
            this.HandleAutomationEvent(senderElement, @event);
        }
    }
}
