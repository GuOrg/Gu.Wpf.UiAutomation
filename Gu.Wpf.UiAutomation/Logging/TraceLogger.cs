namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    [Obsolete("Candidate for removal. Comment on issue #74 if we should keep it.")]
    public class TraceLogger : LoggerBase
    {
        /// <inheritdoc/>
        protected override void GatedDebug(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }

        /// <inheritdoc/>
        protected override void GatedError(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }

        /// <inheritdoc/>
        protected override void GatedFatal(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }

        /// <inheritdoc/>
        protected override void GatedInfo(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }

        /// <inheritdoc/>
        protected override void GatedTrace(string message)
        {
             System.Diagnostics.Trace.WriteLine(message);
        }

        /// <inheritdoc/>
        protected override void GatedWarn(string message)
        {
            System.Diagnostics.Trace.TraceWarning(message);
        }
    }
}
