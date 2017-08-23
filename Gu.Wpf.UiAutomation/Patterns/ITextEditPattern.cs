namespace Gu.Wpf.UiAutomation.Patterns
{
    public interface ITextEditPattern : ITextPattern
    {
        new ITextEditPatternEvents Events { get; }

        ITextRange GetActiveComposition();

        ITextRange GetConversionTarget();
    }
}
