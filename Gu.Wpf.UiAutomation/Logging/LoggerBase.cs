namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    public abstract class LoggerBase : ILogger
    {
        protected LoggerBase()
        {
            this.IsTraceEnabled = false;
            this.IsDebugEnabled = false;

            // Default log level is info and higher
            this.IsInfoEnabled = true;
            this.IsWarnEnabled = true;
            this.IsErrorEnabled = true;
            this.IsFatalEnabled = true;
        }

        /// <inheritdoc/>
        public bool IsTraceEnabled { get; set; }

        /// <inheritdoc/>
        public bool IsDebugEnabled { get; set; }

        /// <inheritdoc/>
        public bool IsInfoEnabled { get; set; }

        /// <inheritdoc/>
        public bool IsWarnEnabled { get; set; }

        /// <inheritdoc/>
        public bool IsErrorEnabled { get; set; }

        /// <inheritdoc/>
        public bool IsFatalEnabled { get; set; }

        /// <inheritdoc/>
        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            this.Log(logLevel, message, null, args);
        }

        /// <inheritdoc/>
        public void Log(LogLevel logLevel, string message, Exception exception, params object[] args)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    if (this.IsTraceEnabled)
                    {
                        this.GatedTrace(GetFormattedMessage(message, exception, args));
                    }

                    break;
                case LogLevel.Debug:
                    if (this.IsDebugEnabled)
                    {
                        this.GatedDebug(GetFormattedMessage(message, exception, args));
                    }

                    break;
                case LogLevel.Info:
                    if (this.IsInfoEnabled)
                    {
                        this.GatedInfo(GetFormattedMessage(message, exception, args));
                    }

                    break;
                case LogLevel.Warn:
                    if (this.IsWarnEnabled)
                    {
                        this.GatedWarn(GetFormattedMessage(message, exception, args));
                    }

                    break;
                case LogLevel.Error:
                    if (this.IsErrorEnabled)
                    {
                        this.GatedError(GetFormattedMessage(message, exception, args));
                    }

                    break;
                case LogLevel.Fatal:
                    if (this.IsFatalEnabled)
                    {
                        this.GatedFatal(GetFormattedMessage(message, exception, args));
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        /// <inheritdoc/>
        public void Trace(string message, params object[] args)
        {
            this.Log(LogLevel.Trace, message, null, args);
        }

        /// <inheritdoc/>
        public void Trace(string message, Exception exception, params object[] args)
        {
            this.Log(LogLevel.Trace, message, exception, args);
        }

        /// <inheritdoc/>
        public void Debug(string message, params object[] args)
        {
            this.Log(LogLevel.Debug, message, null, args);
        }

        /// <inheritdoc/>
        public void Debug(string message, Exception exception, params object[] args)
        {
            this.Log(LogLevel.Debug, message, exception, args);
        }

        /// <inheritdoc/>
        public void Info(string message, params object[] args)
        {
            this.Log(LogLevel.Info, message, null, args);
        }

        /// <inheritdoc/>
        public void Info(string message, Exception exception, params object[] args)
        {
            this.Log(LogLevel.Info, message, exception, args);
        }

        /// <inheritdoc/>
        public void Warn(string message, params object[] args)
        {
            this.Log(LogLevel.Warn, message, null, args);
        }

        /// <inheritdoc/>
        public void Warn(string message, Exception exception, params object[] args)
        {
            this.Log(LogLevel.Warn, message, exception, args);
        }

        /// <inheritdoc/>
        public void Error(string message, params object[] args)
        {
            this.Log(LogLevel.Error, message, null, args);
        }

        /// <inheritdoc/>
        public void Error(string message, Exception exception, params object[] args)
        {
            this.Log(LogLevel.Error, message, exception, args);
        }

        /// <inheritdoc/>
        public void Fatal(string message, params object[] args)
        {
            this.Log(LogLevel.Fatal, message, null, args);
        }

        /// <inheritdoc/>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            this.Log(LogLevel.Fatal, message, exception, args);
        }

        protected abstract void GatedTrace(string message);

        protected abstract void GatedDebug(string message);

        protected abstract void GatedInfo(string message);

        protected abstract void GatedWarn(string message);

        protected abstract void GatedError(string message);

        protected abstract void GatedFatal(string message);

        private static string GetFormattedMessage(string message, Exception exception, params object[] args)
        {
            var formattedMsg = args == null ? message : string.Format(message, args);
            return exception == null ? formattedMsg : string.Concat(formattedMsg, Environment.NewLine, exception);
        }
    }
}
