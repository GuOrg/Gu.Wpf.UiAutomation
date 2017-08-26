namespace Gu.Wpf.UiAutomation.UIA3.EventHandlers
{
    using System;

    public class UIA3FocusChangedEventHandler : FocusChangedEventHandlerBase, Interop.UIAutomationClient.IUIAutomationFocusChangedEventHandler
    {
        public UIA3FocusChangedEventHandler(AutomationBase automation, Action<AutomationElement> callAction)
            : base(automation, callAction)
        {
        }

        public void HandleFocusChangedEvent(Interop.UIAutomationClient.IUIAutomationElement sender)
        {
            var basicAutomationElement = new UIA3BasicAutomationElement((UIA3Automation)this.Automation, sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            this.HandleFocusChangedEvent(senderElement);
        }
    }
}
