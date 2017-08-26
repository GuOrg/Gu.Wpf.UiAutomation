namespace Gu.Wpf.UiAutomation.Logging
{
    public class NullLogger : LoggerBase
    {
        /// <inheritdoc/>
        protected override void GatedTrace(string message)
        {
        }

        /// <inheritdoc/>
        protected override void GatedDebug(string message)
        {
        }

        /// <inheritdoc/>
        protected override void GatedInfo(string message)
        {
        }

        /// <inheritdoc/>
        protected override void GatedWarn(string message)
        {
        }

        /// <inheritdoc/>
        protected override void GatedError(string message)
        {
        }

        /// <inheritdoc/>
        protected override void GatedFatal(string message)
        {
        }
    }
}
