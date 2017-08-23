namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class WindowPatternEvents : IWindowPatternEvents
    {
        public EventId WindowClosedEvent => WindowPattern.WindowClosedEvent;

        public EventId WindowOpenedEvent => WindowPattern.WindowOpenedEvent;
    }
}