using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

namespace Gu.Wpf.UiAutomation.Patterns
{
    public interface ITextChildPattern : IPattern
    {
        AutomationElement TextContainer { get; }
        ITextRange TextRange { get; }
    }
}
