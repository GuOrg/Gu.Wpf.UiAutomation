namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class TextEditPatternEvents : TextPatternEvents, ITextEditPatternEvents
    {
        public EventId ConversionTargetChangedEvent => TextEditPattern.ConversionTargetChangedEvent;

        public EventId TextChangedEvent2 => TextEditPattern.TextChangedEvent2;
    }
}