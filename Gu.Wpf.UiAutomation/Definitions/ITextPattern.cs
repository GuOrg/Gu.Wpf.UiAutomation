namespace Gu.Wpf.UiAutomation
{
    using System.Windows;

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