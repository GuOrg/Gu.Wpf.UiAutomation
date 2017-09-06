namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PropertyNotSupportedException : NotSupportedException
    {
        private const string DefaultMessage = "The requested property is not supported";
        private const string DefaultMessageWithData = "The requested property '{0}' is not supported";

        public PropertyNotSupportedException()
            : base(DefaultMessage)
        {
        }

        public PropertyNotSupportedException(PropertyId property)
            : base(string.Format(DefaultMessageWithData, property.Name))
        {
            this.Property = property;
        }

        public PropertyNotSupportedException(string message, PropertyId property)
            : base(message)
        {
            this.Property = property;
        }

        public PropertyNotSupportedException(PropertyId property, Exception innerException)
            : base(string.Format(DefaultMessageWithData, property.Name), innerException)
        {
            this.Property = property;
        }

        public PropertyNotSupportedException(string message, PropertyId property, Exception innerException)
            : base(message, innerException)
        {
            this.Property = property;
        }

        protected PropertyNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Property = (PropertyId)info.GetValue("Property", typeof(PropertyId));
        }

        public PropertyId Property { get; }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Property", this.Property);
            base.GetObjectData(info, context);
        }
    }
}
