namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    public static class Logger
    {
        private static ILogger @default = new ConsoleLogger();

        public static ILogger Default
        {
            get
            {
                return @default;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                @default = value;
            }
        }
    }
}
