namespace Gu.Wpf.UiAutomation.UIA2.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using Gu.Wpf.UiAutomation.Identifiers;
    using UIA = System.Windows.Automation;

    public class UIA2BasicEventHandler : BasicEventHandlerBase
    {
        public UIA.AutomationEventHandler EventHandler { get; private set; }

        public UIA2BasicEventHandler(AutomationBase automation, Action<AutomationElement, EventId> callAction) : base(automation, callAction)
        {
            EventHandler = HandleAutomationEvent;
        }

        private void HandleAutomationEvent(object sender, UIA.AutomationEventArgs automationEventArgs)
        {
            var basicAutomationElement = new UIA2BasicAutomationElement((UIA2Automation)Automation, (UIA.AutomationElement)sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            var @event = EventId.Find(AutomationType.UIA2, automationEventArgs.EventId.Id);
            HandleAutomationEvent(senderElement, @event);
        }
    }
}
