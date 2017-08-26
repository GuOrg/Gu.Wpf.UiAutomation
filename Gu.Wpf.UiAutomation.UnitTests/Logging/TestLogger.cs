namespace Gu.Wpf.UiAutomation.UnitTests.Logging
{
    using Gu.Wpf.UiAutomation.Logging;

    public class TestLogger : LoggerBase
    {
        public virtual void PublicTrace(string message)
        {
        }

        public virtual void PublicDebug(string message)
        {
        }

        public virtual void PublicInfo(string message)
        {
        }

        public virtual void PublicWarn(string message)
        {
        }

        public virtual void PublicError(string message)
        {
        }

        public virtual void PublicFatal(string message)
        {
        }

        protected override void GatedTrace(string message)
        {
            this.PublicTrace(message);
        }

        protected override void GatedDebug(string message)
        {
            this.PublicDebug(message);
        }

        protected override void GatedInfo(string message)
        {
            this.PublicInfo(message);
        }

        protected override void GatedWarn(string message)
        {
            this.PublicWarn(message);
        }

        protected override void GatedError(string message)
        {
            this.PublicError(message);
        }

        protected override void GatedFatal(string message)
        {
            this.PublicFatal(message);
        }
    }
}
