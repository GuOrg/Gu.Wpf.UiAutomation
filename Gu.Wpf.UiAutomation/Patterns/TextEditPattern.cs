using Gu.Wpf.UiAutomation.Identifiers;

namespace Gu.Wpf.UiAutomation.Patterns
{
    public interface ITextEditPattern : ITextPattern
    {
        new ITextEditPatternEvents Events { get; }

        ITextRange GetActiveComposition();
        ITextRange GetConversionTarget();
    }

    public interface ITextEditPatternEvents : ITextPatternEvents
    {
        EventId ConversionTargetChangedEvent { get; }
        EventId TextChangedEvent2 { get; }
    }
}
