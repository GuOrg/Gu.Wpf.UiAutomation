namespace Gu.Wpf.UiAutomation.UIA3.EventHandlers
{
    using System;

    public class UIA3StructureChangedEventHandler : StructureChangedEventHandlerBase, Interop.UIAutomationClient.IUIAutomationStructureChangedEventHandler
    {
        public UIA3StructureChangedEventHandler(AutomationBase automation, Action<AutomationElement, StructureChangeType, int[]> callAction)
            : base(automation, callAction)
        {
        }

        public void HandleStructureChangedEvent(Interop.UIAutomationClient.IUIAutomationElement sender, Interop.UIAutomationClient.StructureChangeType changeType, int[] runtimeId)
        {
            var basicAutomationElement = new UIA3BasicAutomationElement((UIA3Automation)this.Automation, sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            this.HandleStructureChangedEvent(senderElement, (StructureChangeType)changeType, runtimeId);
        }
    }
}
