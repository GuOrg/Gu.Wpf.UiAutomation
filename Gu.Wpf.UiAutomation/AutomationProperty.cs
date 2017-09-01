namespace Gu.Wpf.UiAutomation
{
    using System;

    /// <summary>
    /// Implementation of the property object.
    /// </summary>
    /// <typeparam name="TVal">The type of the value of the property.</typeparam>
    public class AutomationProperty<TVal> : IAutomationProperty<TVal>, IEquatable<TVal>, IEquatable<AutomationProperty<TVal>>
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
        public TVal ValueOrDefault
        {
            get
            {
                this.TryGetValue(out TVal value);
                return value;
            }
        }

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

        /// <summary>
        /// Implicit operator to convert the property object directly to its value.
        /// </summary>
        /// <param name="automationProperty">The property object which should be converted.</param>
        public static implicit operator TVal(AutomationProperty<TVal> automationProperty)
        {
            return automationProperty == null ? default(TVal) : automationProperty.Value;
        }

        /// <inheritdoc />
        public bool TryGetValue(out TVal value)
        {
            return this.BasicAutomationElement.TryGetPropertyValue(this.PropertyId, out value);
        }

        /// <inheritdoc/>
        public bool Equals(TVal other)
        {
            return object.Equals(this.Value, other);
        }

        /// <inheritdoc/>
        public bool Equals(AutomationProperty<TVal> other)
        {
            return other != null && object.Equals(this.Value, other.Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Convert.ToString(this.ValueOrDefault);
        }
    }
}
