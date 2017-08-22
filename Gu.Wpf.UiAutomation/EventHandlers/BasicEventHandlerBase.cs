namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;

    public abstract class BasicEventHandlerBase : EventHandlerBase, IAutomationEventHandler
    {
        private readonly Action<AutomationElement, EventId> _callAction;

        protected BasicEventHandlerBase(AutomationBase automation, Action<AutomationElement, EventId> callAction)
            : base(automation)
        {
            _callAction = callAction;
        }

        public void HandleAutomationEvent(AutomationElement sender, EventId eventId)
        {
            _callAction(sender, eventId);
        }
    }
}
