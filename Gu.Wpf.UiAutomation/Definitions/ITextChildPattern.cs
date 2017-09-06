namespace Gu.Wpf.UiAutomation
{
    public interface ITextChildPattern : IPattern
    {
        AutomationElement TextContainer { get; }

        ITextRange TextRange { get; }
    }
}
