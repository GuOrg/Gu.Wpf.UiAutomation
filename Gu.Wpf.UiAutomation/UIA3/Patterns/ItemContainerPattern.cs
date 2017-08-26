namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class ItemContainerPattern : PatternBase<Interop.UIAutomationClient.IUIAutomationItemContainerPattern>, IItemContainerPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_ItemContainerPatternId, "ItemContainer", AutomationObjectIds.IsItemContainerPatternAvailableProperty);

        public ItemContainerPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationItemContainerPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public AutomationElement FindItemByProperty(AutomationElement startAfter, PropertyId property, object value)
        {
            var foundNativeElement = ComCallWrapper.Call(
                () =>
                    this.NativePattern.FindItemByProperty(
                        startAfter?.ToNative(),
                        property?.Id ?? 0,
                        ValueConverter.ToNative(value)));
            return AutomationElementConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, foundNativeElement);
        }
    }
}
