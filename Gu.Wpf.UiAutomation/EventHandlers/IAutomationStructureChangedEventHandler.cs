namespace Gu.Wpf.UiAutomation
{
    public interface IAutomationStructureChangedEventHandler
    {
        void HandleStructureChangedEvent(AutomationElement sender, StructureChangeType changeType, int[] runtimeId);
    }
}
