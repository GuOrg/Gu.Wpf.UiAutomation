using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gu.Wpf.UiAutomation.Exceptions
{
    [Serializable]
    public class UiAutomationException : Exception
    {
        public UiAutomationException()
        {
        }

        public UiAutomationException(string message)
            : base(message)
        {
        }

        public UiAutomationException(Exception innerException)
            : base(String.Empty, innerException)
        {
        }

        public UiAutomationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected UiAutomationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
