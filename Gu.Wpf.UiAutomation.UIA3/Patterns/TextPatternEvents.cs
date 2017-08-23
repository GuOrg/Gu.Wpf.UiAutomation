namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class TextPatternEvents : ITextPatternEvents
    {
        public EventId TextChangedEvent => TextPattern.TextChangedEvent;

        public EventId TextSelectionChangedEvent => TextPattern.TextSelectionChangedEvent;
    }
}