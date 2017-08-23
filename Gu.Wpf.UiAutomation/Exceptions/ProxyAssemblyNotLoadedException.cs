namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class ProxyAssemblyNotLoadedException : UiAutomationException
    {
        public ProxyAssemblyNotLoadedException()
        {
        }

        public ProxyAssemblyNotLoadedException(string message)
            : base(message)
        {
        }

        public ProxyAssemblyNotLoadedException(Exception innerException)
            : base(string.Empty, innerException)
        {
        }

        public ProxyAssemblyNotLoadedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ProxyAssemblyNotLoadedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
