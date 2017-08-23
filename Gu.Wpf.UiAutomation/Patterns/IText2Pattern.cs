namespace Gu.Wpf.UiAutomation
{
    public interface IText2Pattern : ITextPattern
    {
        ITextRange GetCaretRange(out bool isActive);

        ITextRange RangeFromAnnotation(AutomationElement annotation);
    }
}
