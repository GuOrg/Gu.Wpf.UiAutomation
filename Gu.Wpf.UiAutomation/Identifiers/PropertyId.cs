namespace Gu.Wpf.UiAutomation
{
    using System;

    /// <summary>
    /// A wrapper around the property ids
    /// </summary>
    public class PropertyId : ConvertibleIdentifierBase
    {
        /// <summary>
        /// Fixed PropertyId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly PropertyId NotSupportedByFramework = new PropertyId(-1, "Not supported");

        public PropertyId(int id, string name)
            : base(id, name)
        {
        }

        public PropertyId SetConverter(Func<AutomationBase, object, object> convertMethod)
        {
            return this.SetConverter<PropertyId>(convertMethod);
        }

        /// <summary>
        /// Returns a condition for this property with the given value
        /// </summary>
        public PropertyCondition GetCondition(object value)
        {
            return new PropertyCondition(this, value);
        }

        public static PropertyId Register(int id, string name)
        {
            return RegisterProperty(id, name);
        }

        public static PropertyId Find(int id)
        {
            return FindProperty(id);
        }
    }
}
