namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.Identifiers;

    public abstract class PropertyChangedEventHandlerBase : EventHandlerBase, IAutomationPropertyChangedEventHandler
    {
        private readonly Action<AutomationElement, PropertyId, object> callAction;

        protected PropertyChangedEventHandlerBase(AutomationBase automation, Action<AutomationElement, PropertyId, object> callAction)
            : base(automation)
        {
            this.callAction = callAction;
        }

        /// <inheritdoc/>
        public void HandlePropertyChangedEvent(AutomationElement sender, PropertyId propertyId, object newValue)
        {
            this.callAction(sender, propertyId, newValue);
        }
    }
}
