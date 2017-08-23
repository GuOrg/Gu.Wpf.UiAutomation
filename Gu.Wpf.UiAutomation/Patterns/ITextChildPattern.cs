namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ITextChildPattern : IPattern
    {
        AutomationElement TextContainer { get; }

        ITextRange TextRange { get; }
    }
}
