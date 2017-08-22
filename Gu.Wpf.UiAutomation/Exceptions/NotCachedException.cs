namespace Gu.Wpf.UiAutomation.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class NotCachedException : UiAutomationException
    {
        public NotCachedException()
        {
        }

        public NotCachedException(string message)
            : base(message)
        {
        }

        public NotCachedException(Exception innerException)
            : base(String.Empty, innerException)
        {
        }

        public NotCachedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NotCachedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
