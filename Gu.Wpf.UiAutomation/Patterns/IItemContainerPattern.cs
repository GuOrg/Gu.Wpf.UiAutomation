namespace Gu.Wpf.UiAutomation
{
    public interface IItemContainerPattern : IPattern
    {
        AutomationElement FindItemByProperty(AutomationElement startAfter, PropertyId property, object value);
    }
}
