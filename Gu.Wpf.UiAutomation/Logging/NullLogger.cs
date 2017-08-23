namespace Gu.Wpf.UiAutomation.Logging
{
    public class NullLogger : LoggerBase
    {
        /// <inheritdoc/>
        protected internal override void GatedTrace(string message)
        {
        }

        /// <inheritdoc/>
        protected internal override void GatedDebug(string message)
        {
        }

        /// <inheritdoc/>
        protected internal override void GatedInfo(string message)
        {
        }

        /// <inheritdoc/>
        protected internal override void GatedWarn(string message)
        {
        }

        /// <inheritdoc/>
        protected internal override void GatedError(string message)
        {
        }

        /// <inheritdoc/>
        protected internal override void GatedFatal(string message)
        {
        }
    }
}
