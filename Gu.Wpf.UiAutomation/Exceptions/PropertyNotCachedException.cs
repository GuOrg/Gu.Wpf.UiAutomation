namespace Gu.Wpf.UiAutomation.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using Gu.Wpf.UiAutomation.Identifiers;

    [Serializable]
    public class PropertyNotCachedException : NotCachedException
    {
        private const string DefaultMessage = "The requested property is not cached";
        private const string DefaultMessageWithData = "The requested property '{0}' is not cached";

        public PropertyNotCachedException()
            : base(DefaultMessage)
        {
        }

        public PropertyNotCachedException(PropertyId property)
            : base(string.Format(DefaultMessageWithData, property))
        {
            this.Property = property;
        }

        public PropertyNotCachedException(string message, PropertyId property)
            : base(message)
        {
            this.Property = property;
        }

        public PropertyNotCachedException(PropertyId property, Exception innerException)
            : base(string.Format(DefaultMessageWithData, property), innerException)
        {
            this.Property = property;
        }

        public PropertyNotCachedException(string message, PropertyId property, Exception innerException)
            : base(message, innerException)
        {
            this.Property = property;
        }

        protected PropertyNotCachedException(SerializationInfo info, StreamingContext context)
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
