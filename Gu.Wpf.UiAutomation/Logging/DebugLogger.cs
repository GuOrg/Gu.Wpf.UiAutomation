namespace Gu.Wpf.UiAutomation.Logging
{
    public class DebugLogger : LoggerBase
    {
        /// <inheritdoc/>
        protected internal override void GatedDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <inheritdoc/>
        protected internal override void GatedError(string message)
        {
            System.Diagnostics.Debug.Fail(message);
        }

        /// <inheritdoc/>
        protected internal override void GatedFatal(string message)
        {
            System.Diagnostics.Debug.Fail(message);
        }

        /// <inheritdoc/>
        protected internal override void GatedInfo(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <inheritdoc/>
        protected internal override void GatedTrace(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <inheritdoc/>
        protected internal override void GatedWarn(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
