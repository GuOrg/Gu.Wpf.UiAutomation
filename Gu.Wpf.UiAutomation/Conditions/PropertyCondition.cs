namespace Gu.Wpf.UiAutomation.Conditions
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;

    public class PropertyCondition : ConditionBase
    {
        public PropertyCondition(PropertyId property, object value)
            : this(property, value, PropertyConditionFlags.None)
        {
        }

        public PropertyCondition(PropertyId property, object value, PropertyConditionFlags propertyConditionFlags)
        {
            Property = property;
            Value = value;
            PropertyConditionFlags = propertyConditionFlags;
        }

        public PropertyId Property { get; }

        public PropertyConditionFlags PropertyConditionFlags { get; private set; }

        public object Value { get; }

        public override string ToString()
        {
            return $"{Property}: {Value}";
        }
    }
}
