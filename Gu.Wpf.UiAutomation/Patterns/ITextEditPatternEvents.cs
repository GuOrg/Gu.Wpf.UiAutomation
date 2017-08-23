namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ITextEditPatternEvents : ITextPatternEvents
    {
        EventId ConversionTargetChangedEvent { get; }

        EventId TextChangedEvent2 { get; }
    }
}