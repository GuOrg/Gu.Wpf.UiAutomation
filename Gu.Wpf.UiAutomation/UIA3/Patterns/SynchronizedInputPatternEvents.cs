namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    public class SynchronizedInputPatternEvents : ISynchronizedInputPatternEvents
    {
        public EventId DiscardedEvent => SynchronizedInputPattern.DiscardedEvent;

        public EventId ReachedOtherElementEvent => SynchronizedInputPattern.ReachedOtherElementEvent;

        public EventId ReachedTargetEvent => SynchronizedInputPattern.ReachedTargetEvent;
    }
}