namespace Gu.Wpf.UiAutomation
{
    public interface ITextEditPatternEvents : ITextPatternEvents
    {
        EventId ConversionTargetChangedEvent { get; }

        EventId TextChangedEvent2 { get; }
    }
}