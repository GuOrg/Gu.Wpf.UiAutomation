namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// A wrapper around the pattern ids
    /// </summary>
    public class PatternId : IdentifierBase
    {
        /// <summary>
        /// Fixed PatternId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly PatternId NotSupportedByFramework = new PatternId(-1, "Not supported", null);

        public PatternId(int id, string name, PropertyId availabilityProperty)
            : base(id, name)
        {
            this.AvailabilityProperty = availabilityProperty;
        }

        /// <summary>
        /// Property which can be used to check for the patterns availability on an element
        /// </summary>
        public PropertyId AvailabilityProperty { get; }

        public static PatternId Register(int id, string name, PropertyId availabilityProperty)
        {
            return RegisterPattern(id, name, availabilityProperty);
        }

        public static PatternId Find(int id)
        {
            return FindPattern(id);
        }
    }
}
