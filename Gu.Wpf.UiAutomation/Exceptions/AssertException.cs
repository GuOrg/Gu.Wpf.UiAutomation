namespace Gu.Wpf.UiAutomation
{
    using System;

    /// <summary>
    /// Thrown when an assertion failed.
    /// </summary>
    [Serializable]
    public class AssertException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class.
        /// </summary>
        public AssertException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class.
        /// </summary>
        /// <param name="message"> The error message that explains the reason for the exception.</param>
        public AssertException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class.
        /// </summary>
        /// <param name="message"> The error message that explains the reason for the exception.</param>
        /// <param name="innerException"> The exception that caused the current exception.</param>
        public AssertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AssertException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Create an <see cref="AssertException"/> or an exception specific to the currently used test framework if found.
        /// </summary>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <returns>The exception.</returns>
        public static Exception Create(string message)
        {
            return new AssertException(message);
        }

        /// <summary>
        /// Create an <see cref="AssertException"/> or an exception specific to the currently used test framework if found.
        /// </summary>
        /// <param name="message">The message explaining why the assertion failed.</param>
        /// <param name="innerException"> The exception that caused the current exception.</param>
        /// <returns>The exception.</returns>
        public static Exception Create(string message, Exception innerException)
        {
            return new AssertException(message, innerException);
        }
    }
}
