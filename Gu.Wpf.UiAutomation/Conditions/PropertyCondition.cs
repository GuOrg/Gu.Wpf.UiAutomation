namespace Gu.Wpf.UiAutomation
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Interop.UIAutomationClient;

    public class PropertyCondition : ConditionBase
    {
        public PropertyCondition(PropertyId property, object value)
            : this(property, value, PropertyConditionFlags.None)
        {
        }

        public PropertyCondition(PropertyId property, object value, PropertyConditionFlags propertyConditionFlags)
        {
            this.Property = property;
            this.Value = value;
            this.PropertyConditionFlags = propertyConditionFlags;
        }

        public PropertyId Property { get; }

        public PropertyConditionFlags PropertyConditionFlags { get; }

        public object Value { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Property.Name}: {this.Value}";
        }

        public override IUIAutomationCondition ToNative(IUIAutomation automation)
        {
            return automation.CreatePropertyConditionEx(this.Property.Id, ValueConverter.ToNative(this.Value), (Interop.UIAutomationClient.PropertyConditionFlags)this.PropertyConditionFlags);
        }
    }
}
