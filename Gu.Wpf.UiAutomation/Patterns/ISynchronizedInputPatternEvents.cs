namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;

    public interface ISynchronizedInputPatternEvents
    {
        EventId DiscardedEvent { get; }

        EventId ReachedOtherElementEvent { get; }

        EventId ReachedTargetEvent { get; }
    }
}