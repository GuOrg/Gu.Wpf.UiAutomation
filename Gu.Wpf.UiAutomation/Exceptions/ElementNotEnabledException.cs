namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class ElementNotEnabledException : UiAutomationException
    {
        public ElementNotEnabledException()
        {
        }

        public ElementNotEnabledException(string message)
            : base(message)
        {
        }

        public ElementNotEnabledException(Exception innerException)
            : base(string.Empty, innerException)
        {
        }

        public ElementNotEnabledException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ElementNotEnabledException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
