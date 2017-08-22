namespace Gu.Wpf.UiAutomation.EventHandlers
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Definitions;

    public interface IAutomationStructureChangedEventHandler
    {
        void HandleStructureChangedEvent(AutomationElement sender, StructureChangeType changeType, int[] runtimeId);
    }
}
