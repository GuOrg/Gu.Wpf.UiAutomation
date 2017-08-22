namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class ItemContainerPattern : PatternBase<UIA.IUIAutomationItemContainerPattern>, IItemContainerPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_ItemContainerPatternId, "ItemContainer", AutomationObjectIds.IsItemContainerPatternAvailableProperty);

        public ItemContainerPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationItemContainerPattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public AutomationElement FindItemByProperty(AutomationElement startAfter, PropertyId property, object value)
        {
            var foundNativeElement = ComCallWrapper.Call(() =>
                NativePattern.FindItemByProperty(
                    startAfter?.ToNative(),
                    property?.Id ?? 0, ValueConverter.ToNative(value)));
            return AutomationElementConverter.NativeToManaged((UIA3Automation)BasicAutomationElement.Automation, foundNativeElement);
        }
    }
}
