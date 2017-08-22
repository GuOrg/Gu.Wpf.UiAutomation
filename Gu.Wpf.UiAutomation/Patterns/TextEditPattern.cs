namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

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
