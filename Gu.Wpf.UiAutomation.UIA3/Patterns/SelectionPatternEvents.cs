namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class SelectionPatternEvents : ISelectionPatternEvents
    {
        public EventId InvalidatedEvent => SelectionPattern.InvalidatedEvent;
    }
}