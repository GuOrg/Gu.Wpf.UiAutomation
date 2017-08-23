namespace Gu.Wpf.UiAutomation
{
    using System;

    public abstract class BasicEventHandlerBase : EventHandlerBase, IAutomationEventHandler
    {
        private readonly Action<AutomationElement, EventId> callAction;

        protected BasicEventHandlerBase(AutomationBase automation, Action<AutomationElement, EventId> callAction)
            : base(automation)
        {
            this.callAction = callAction;
        }

        /// <inheritdoc/>
        public void HandleAutomationEvent(AutomationElement sender, EventId eventId)
        {
            this.callAction(sender, eventId);
        }
    }
}
