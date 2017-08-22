namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;

    public interface IText2Pattern : ITextPattern
    {
        ITextRange GetCaretRange(out bool isActive);
        ITextRange RangeFromAnnotation(AutomationElement annotation);
    }
}
