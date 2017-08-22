namespace Gu.Wpf.UiAutomation.UIA2.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.EventHandlers;
    using UIA = System.Windows.Automation;

    public class UIA2StructureChangedEventHandler : StructureChangedEventHandlerBase
    {
        public UIA.StructureChangedEventHandler EventHandler { get; private set; }

        public UIA2StructureChangedEventHandler(AutomationBase automation, Action<AutomationElement, StructureChangeType, int[]> callAction) : base(automation, callAction)
        {
            EventHandler = HandleStructureChangedEvent;
        }

        private void HandleStructureChangedEvent(object sender, UIA.StructureChangedEventArgs structureChangedEventArgs)
        {
            var basicAutomationElement = new UIA2BasicAutomationElement((UIA2Automation)Automation, (UIA.AutomationElement)sender);
            var senderElement = new AutomationElement(basicAutomationElement);
            HandleStructureChangedEvent(senderElement, (StructureChangeType)structureChangedEventArgs.StructureChangeType, structureChangedEventArgs.GetRuntimeId());
        }
    }
}
