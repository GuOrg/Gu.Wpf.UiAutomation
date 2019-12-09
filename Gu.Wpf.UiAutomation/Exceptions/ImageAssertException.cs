namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown when ImageAssert.AreEqual fails.
    /// </summary>
    [Serializable]
#pragma warning disable CA1032 // Implement standard exception constructors
    public class ImageAssertException : AssertException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAssertException"/> class.
        /// </summary>
        /// <param name="expected">The expected <see cref="Bitmap"/>.</param>
        /// <param name="actual">The actual <see cref="Bitmap"/>.</param>
        /// <param name="message">The message.</param>
        /// <param name="fileName">The file or resource name.</param>
        public ImageAssertException(Bitmap? expected, Bitmap actual, string message, string? fileName)
            : base(message)
        {
            this.Expected = expected;
            this.Actual = actual;
            this.FileName = fileName;
        }

        protected ImageAssertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Expected = (Bitmap?)info.GetValue(nameof(this.Expected), typeof(Bitmap))!;
            this.Actual = (Bitmap)info.GetValue(nameof(this.Actual), typeof(Bitmap))!;
            this.FileName = info.GetString(nameof(this.FileName));
        }

        public Bitmap? Expected { get; }

        public Bitmap Actual { get; }

        public string? FileName { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(this.Expected), this.Expected);
            info.AddValue(nameof(this.Actual), this.Actual);
            info.AddValue(nameof(this.FileName), this.FileName);
            base.GetObjectData(info, context);
        }
    }
}
