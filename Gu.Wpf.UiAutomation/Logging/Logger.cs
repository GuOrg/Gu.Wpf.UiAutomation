namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    public static class Logger
    {
        private static ILogger @default = new ConsoleLogger();

        public static ILogger Default
        {
            get => @default;
            set => @default = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
