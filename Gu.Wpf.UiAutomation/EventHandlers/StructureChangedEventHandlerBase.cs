namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using System;
    using Gu.Wpf.UiAutomation.Definitions;

    public abstract class StructureChangedEventHandlerBase : EventHandlerBase, IAutomationStructureChangedEventHandler
    {
        private readonly Action<AutomationElement, StructureChangeType, int[]> callAction;

        protected StructureChangedEventHandlerBase(AutomationBase automation, Action<AutomationElement, StructureChangeType, int[]> callAction)
            : base(automation)
        {
            this.callAction = callAction;
        }

        /// <inheritdoc/>
        public void HandleStructureChangedEvent(AutomationElement sender, StructureChangeType changeType, int[] runtimeId)
        {
            this.callAction(sender, changeType, runtimeId);
        }
    }
}
