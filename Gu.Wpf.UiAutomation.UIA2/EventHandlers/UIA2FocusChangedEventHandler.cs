using System;
using Gu.Wpf.UiAutomation;
using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.EventHandlers;
using UIA = System.Windows.Automation;

namespace Gu.Wpf.UiAutomation.UIA2.EventHandlers
{
    public class UIA2FocusChangedEventHandler : FocusChangedEventHandlerBase
    {
        public UIA.AutomationFocusChangedEventHandler EventHandler { get; private set; }

        public UIA2FocusChangedEventHandler(AutomationBase automation, Action<AutomationElement> callAction) : base(automation, callAction)
        {
            EventHandler = HandleFocusChangedEvent;
        }

        private void HandleFocusChangedEvent(object sender, UIA.AutomationFocusChangedEventArgs automationFocusChangedEventArgs)
        {
            var basicAutomationElement = new UIA2BasicAutomationElement((UIA2Automation)Automation, (UIA.AutomationElement)sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            HandleFocusChangedEvent(senderElement);
        }
    }
}
