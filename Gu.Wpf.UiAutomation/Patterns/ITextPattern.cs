namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.Shapes;

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