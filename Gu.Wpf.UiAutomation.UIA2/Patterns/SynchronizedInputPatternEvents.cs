namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;

    public class SynchronizedInputPatternEvents : ISynchronizedInputPatternEvents
    {
        public EventId DiscardedEvent => SynchronizedInputPattern.DiscardedEvent;

        public EventId ReachedOtherElementEvent => SynchronizedInputPattern.ReachedOtherElementEvent;

        public EventId ReachedTargetEvent => SynchronizedInputPattern.ReachedTargetEvent;
    }
}