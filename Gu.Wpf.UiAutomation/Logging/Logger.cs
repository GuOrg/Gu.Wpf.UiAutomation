namespace Gu.Wpf.UiAutomation.Logging
{
    using System;

    [Obsolete("Candidate for removal. Comment on issue #74 if we should keep it.")]
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
