using System;
using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

namespace Gu.Wpf.UiAutomation.EventHandlers
{
    public abstract class FocusChangedEventHandlerBase : EventHandlerBase, IAutomationFocusChangedEventHandler
    {
        private readonly Action<AutomationElement> _callAction;

        protected FocusChangedEventHandlerBase(AutomationBase automation, Action<AutomationElement> callAction)
            : base(automation)
        {
            _callAction = callAction;
        }

        public void HandleFocusChangedEvent(AutomationElement sender)
        {
            _callAction(sender);
        }
    }
}
