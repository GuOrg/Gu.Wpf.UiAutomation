namespace Gu.Wpf.UiAutomation.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class NotSupportedByFrameworkException : UiAutomationException
    {
        private const string DefaultMessage = "The requested pattern or property is not supported by the choosen framework. Consider using a newer framework.";

        public NotSupportedByFrameworkException() : base(DefaultMessage)
        {
        }

        public NotSupportedByFrameworkException(string message)
            : base(message)
        {
        }

        public NotSupportedByFrameworkException(Exception innerException)
            : base(DefaultMessage, innerException)
        {
        }

        public NotSupportedByFrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NotSupportedByFrameworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
