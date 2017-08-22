using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Identifiers;
using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

namespace Gu.Wpf.UiAutomation.Patterns
{
    public interface IItemContainerPattern : IPattern
    {
        AutomationElement FindItemByProperty(AutomationElement startAfter, PropertyId property, object value);
    }
}
