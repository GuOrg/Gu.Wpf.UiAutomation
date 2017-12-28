namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;
    using Gu.Wpf.UiAutomation.UIA3.Converters;

    public class PropertyCondition : ConditionBase
    {
        public PropertyCondition(AutomationProperty property, object value)
            : this(property, value, PropertyConditionFlags.None)
        {
        }

        public PropertyCondition(AutomationProperty property, object value, PropertyConditionFlags propertyConditionFlags)
        {
            this.Property = property;
            this.Value = value;
            this.PropertyConditionFlags = propertyConditionFlags;
        }

        public AutomationProperty Property { get; }

        public PropertyConditionFlags PropertyConditionFlags { get; }

        public object Value { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Property.ProgrammaticName}: {this.Value}";
        }

        public override Condition ToNative()
        {
            return new System.Windows.Automation.PropertyCondition(
                this.Property,
                ValueConverter.ToNative(this.Value),
                this.PropertyConditionFlags);
        }
    }
}
