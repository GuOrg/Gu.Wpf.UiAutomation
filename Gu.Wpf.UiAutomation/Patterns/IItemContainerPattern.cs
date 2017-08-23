namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface IItemContainerPattern : IPattern
    {
        AutomationElement FindItemByProperty(AutomationElement startAfter, PropertyId property, object value);
    }
}
