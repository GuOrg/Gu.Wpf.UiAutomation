namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using System;

    public abstract class FocusChangedEventHandlerBase : EventHandlerBase, IAutomationFocusChangedEventHandler
    {
        private readonly Action<AutomationElement> callAction;

        protected FocusChangedEventHandlerBase(AutomationBase automation, Action<AutomationElement> callAction)
            : base(automation)
        {
            this.callAction = callAction;
        }

        /// <inheritdoc/>
        public void HandleFocusChangedEvent(AutomationElement sender)
        {
            this.callAction(sender);
        }
    }
}
