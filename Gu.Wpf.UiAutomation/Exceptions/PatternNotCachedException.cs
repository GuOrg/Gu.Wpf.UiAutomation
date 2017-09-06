namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PatternNotCachedException : NotCachedException
    {
        private const string DefaultMessage = "The requested pattern is not cached";
        private const string DefaultMessageWithData = "The requested pattern '{0}' is not cached";

        public PatternNotCachedException()
            : base(DefaultMessage)
        {
        }

        public PatternNotCachedException(PatternId pattern)
            : base(string.Format(DefaultMessageWithData, pattern.Name))
        {
            this.Pattern = pattern;
        }

        public PatternNotCachedException(string message, PatternId pattern)
            : base(message)
        {
            this.Pattern = pattern;
        }

        public PatternNotCachedException(PatternId pattern, Exception innerException)
            : base(string.Format(DefaultMessageWithData, pattern.Name), innerException)
        {
            this.Pattern = pattern;
        }

        public PatternNotCachedException(string message, PatternId pattern, Exception innerException)
            : base(message, innerException)
        {
            this.Pattern = pattern;
        }

        protected PatternNotCachedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Pattern = (PatternId)info.GetValue("Pattern", typeof(PatternId));
        }

        public PatternId Pattern { get; }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Pattern", this.Pattern);
            base.GetObjectData(info, context);
        }
    }
}
