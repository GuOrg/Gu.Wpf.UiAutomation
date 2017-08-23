namespace Gu.Wpf.UiAutomation
{
    public interface ISynchronizedInputPatternEvents
    {
        EventId DiscardedEvent { get; }

        EventId ReachedOtherElementEvent { get; }

        EventId ReachedTargetEvent { get; }
    }
}