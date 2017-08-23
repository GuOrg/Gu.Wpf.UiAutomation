namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class TextEditPatternEvents : TextPatternEvents, ITextEditPatternEvents
    {
        public EventId ConversionTargetChangedEvent => EventId.NotSupportedByFramework;

        public EventId TextChangedEvent2 => EventId.NotSupportedByFramework;
    }
}