namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class NotSupportedException : UiAutomationException
    {
        public NotSupportedException()
        {
        }

        public NotSupportedException(string message)
            : base(message)
        {
        }

        public NotSupportedException(Exception innerException)
            : base(string.Empty, innerException)
        {
        }

        public NotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
