namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    public class ConsoleLogger : LoggerBase
    {
        /// <inheritdoc/>
        protected override void GatedDebug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <inheritdoc/>
        protected override void GatedError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        /// <inheritdoc/>
        protected override void GatedFatal(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        /// <inheritdoc/>
        protected override void GatedInfo(string message)
        {
            Console.WriteLine(message);
        }

        /// <inheritdoc/>
        protected override void GatedTrace(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <inheritdoc/>
        protected override void GatedWarn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
