namespace Gu.Wpf.UiAutomation.UIA2.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using UIA = System.Windows.Automation;

    public class UIA2FocusChangedEventHandler : FocusChangedEventHandlerBase
    {
        public UIA.AutomationFocusChangedEventHandler EventHandler { get; private set; }

        public UIA2FocusChangedEventHandler(AutomationBase automation, Action<AutomationElement> callAction)
            : base(automation, callAction)
        {
            this.EventHandler = this.HandleFocusChangedEvent;
        }

        private void HandleFocusChangedEvent(object sender, UIA.AutomationFocusChangedEventArgs automationFocusChangedEventArgs)
        {
            var basicAutomationElement = new UIA2BasicAutomationElement((UIA2Automation)this.Automation, (UIA.AutomationElement)sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            this.HandleFocusChangedEvent(senderElement);
        }
    }
}
