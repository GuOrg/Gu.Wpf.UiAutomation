namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class InvokePatternEvents : IInvokePatternEvents
    {
        public EventId InvokedEvent => InvokePattern.InvokedEvent;
    }
}