namespace Gu.Wpf.UiAutomation.UIA2.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using Gu.Wpf.UiAutomation.Identifiers;
    using UIA = System.Windows.Automation;

    public class UIA2PropertyChangedEventHandler : PropertyChangedEventHandlerBase
    {
        public UIA.AutomationPropertyChangedEventHandler EventHandler { get; private set; }

        public UIA2PropertyChangedEventHandler(AutomationBase automation, Action<AutomationElement, PropertyId, object> callAction) : base(automation, callAction)
        {
            this.EventHandler = this.HandlePropertyChangedEvent;
        }

        private void HandlePropertyChangedEvent(object sender, UIA.AutomationPropertyChangedEventArgs automationPropertyChangedEventArgs)
        {
            var basicAutomationElement = new UIA2BasicAutomationElement((UIA2Automation)this.Automation, (UIA.AutomationElement)sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            var propertyId = PropertyId.Find(this.Automation.AutomationType, automationPropertyChangedEventArgs.Property.Id);
            this.HandlePropertyChangedEvent(senderElement, propertyId, automationPropertyChangedEventArgs.NewValue);
        }
    }
}
