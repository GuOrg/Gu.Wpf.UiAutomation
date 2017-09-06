namespace Gu.Wpf.UiAutomation
{
    public interface ITextEditPattern : ITextPattern
    {
        new ITextEditPatternEvents Events { get; }

        ITextRange GetActiveComposition();

        ITextRange GetConversionTarget();
    }
}
