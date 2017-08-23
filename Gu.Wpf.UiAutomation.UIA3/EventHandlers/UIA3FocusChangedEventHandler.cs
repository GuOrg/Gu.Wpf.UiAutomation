namespace Gu.Wpf.UiAutomation.UIA3.EventHandlers
{
    using System;
    using UIA = Interop.UIAutomationClient;

    public class UIA3FocusChangedEventHandler : FocusChangedEventHandlerBase, UIA.IUIAutomationFocusChangedEventHandler
    {
        public UIA3FocusChangedEventHandler(AutomationBase automation, Action<AutomationElement> callAction)
            : base(automation, callAction)
        {
        }

        public void HandleFocusChangedEvent(UIA.IUIAutomationElement sender)
        {
            var basicAutomationElement = new UIA3BasicAutomationElement((UIA3Automation)this.Automation, sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            this.HandleFocusChangedEvent(senderElement);
        }
    }
}
