namespace Gu.Wpf.UiAutomation
{
    public interface ITextPattern : IPattern
    {
        ITextPatternEvents Events { get; }

        ITextRange DocumentRange { get; }

        SupportedTextSelection SupportedTextSelection { get; }

        ITextRange[] GetSelection();

        ITextRange[] GetVisibleRanges();

        ITextRange RangeFromChild(AutomationElement child);

        ITextRange RangeFromPoint(Point point);
    }
}