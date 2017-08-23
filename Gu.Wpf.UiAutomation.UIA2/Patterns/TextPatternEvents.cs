namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class TextPatternEvents : ITextPatternEvents
    {
        public EventId TextChangedEvent => TextPattern.TextChangedEvent;

        public EventId TextSelectionChangedEvent => TextPattern.TextSelectionChangedEvent;
    }
}