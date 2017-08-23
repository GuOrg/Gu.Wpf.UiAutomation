namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class ElementNotAvailableException : UiAutomationException
    {
        public ElementNotAvailableException()
        {
        }

        public ElementNotAvailableException(string message)
            : base(message)
        {
        }

        public ElementNotAvailableException(Exception innerException)
            : base(string.Empty, innerException)
        {
        }

        public ElementNotAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ElementNotAvailableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
