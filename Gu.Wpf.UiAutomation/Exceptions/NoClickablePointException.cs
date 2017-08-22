using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gu.Wpf.UiAutomation.Exceptions
{
    [Serializable]
    public class NoClickablePointException : UiAutomationException
    {
        public NoClickablePointException()
        {
        }

        public NoClickablePointException(string message)
            : base(message)
        {
        }

        public NoClickablePointException(Exception innerException)
            : base(String.Empty, innerException)
        {
        }

        public NoClickablePointException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NoClickablePointException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
