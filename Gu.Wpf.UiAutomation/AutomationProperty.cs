namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// Implementation of the property object.
    /// </summary>
    /// <typeparam name="TVal">The type of the value of the property.</typeparam>
    public class AutomationProperty<TVal> : IAutomationProperty<TVal>
    {
        /// <summary>
        /// Create the property object.
        /// </summary>
        /// <param name="propertyId">The <see cref="PropertyId"/> for this property object.</param>
        /// <param name="basicAutomationElement">The <see cref="BasicAutomationElement"/> for this property object.</param>
        public AutomationProperty(PropertyId propertyId, BasicAutomationElementBase basicAutomationElement)
        {
            this.PropertyId = propertyId;
            this.BasicAutomationElement = basicAutomationElement;
        }

        /// <inheritdoc />
        public TVal Value => this.BasicAutomationElement.GetPropertyValue<TVal>(this.PropertyId);

        /// <inheritdoc />
        public bool IsSupported => this.TryGetValue(out TVal _);

        /// <summary>
        /// The <see cref="PropertyId"/> of this property object.
        /// </summary>
        protected PropertyId PropertyId { get; }

        /// <summary>
        /// The <see cref="BasicAutomationElement"/> where this property object belongs to.
        /// </summary>
        protected BasicAutomationElementBase BasicAutomationElement { get; }

        /// <inheritdoc />
        public TVal ValueOrDefault(TVal @default = default(TVal))
        {
            if (this.TryGetValue(out var value))
            {
                return value;
            }

            return @default;
        }

        /// <inheritdoc />
        public bool TryGetValue(out TVal value)
        {
            return this.BasicAutomationElement.TryGetPropertyValue(this.PropertyId, out value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.ValueOrDefault()?.ToString() ?? "null";
        }
    }
}
