#if !NET35
using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Identifiers;
using Gu.Wpf.UiAutomation.Patterns;
using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
using Gu.Wpf.UiAutomation.UIA2.Converters;
using Gu.Wpf.UiAutomation.UIA2.Identifiers;
using UIA = System.Windows.Automation;

namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    public class ItemContainerPattern : PatternBase<UIA.ItemContainerPattern>, IItemContainerPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.ItemContainerPattern.Pattern.Id, "ItemContainer", AutomationObjectIds.IsItemContainerPatternAvailableProperty);

        public ItemContainerPattern(BasicAutomationElementBase basicAutomationElement, UIA.ItemContainerPattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public AutomationElement FindItemByProperty(AutomationElement startAfter, PropertyId property, object value)
        {
            var foundNativeElement = this.NativePattern.FindItemByProperty(
                    startAfter?.ToNative(),
                    property == null ? null : UIA.AutomationProperty.LookupById(property.Id), ValueConverter.ToNative(value));
            return AutomationElementConverter.NativeToManaged((UIA2Automation)this.BasicAutomationElement.Automation, foundNativeElement);
        }
    }
}
#endif
