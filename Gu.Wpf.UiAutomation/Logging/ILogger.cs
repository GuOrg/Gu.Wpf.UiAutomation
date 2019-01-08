namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    /// <summary>
    /// Not sure if we want to keep the logging stuff.
    /// </summary>
    public interface ILogger
    {
        bool IsTraceEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarnEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsFatalEnabled { get; }

        void Log(LogLevel logLevel, string message, params object[] args);

        void Log(LogLevel logLevel, string message, Exception exception, params object[] args);

        void Trace(string message, params object[] args);

        void Trace(string message, Exception exception, params object[] args);

        void Debug(string message, params object[] args);

        void Debug(string message, Exception exception, params object[] args);

        void Info(string message, params object[] args);

        void Info(string message, Exception exception, params object[] args);

        void Warn(string message, params object[] args);

        void Warn(string message, Exception exception, params object[] args);

#pragma warning disable CA1716 // Identifiers should not match keywords
        void Error(string message, params object[] args);

        void Error(string message, Exception exception, params object[] args);
#pragma warning restore CA1716 // Identifiers should not match keywords

        void Fatal(string message, params object[] args);

        void Fatal(string message, Exception exception, params object[] args);
    }
}
