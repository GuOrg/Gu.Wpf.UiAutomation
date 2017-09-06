namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class SynchronizedInputPattern : SynchronizedInputPatternBase<Interop.UIAutomationClient.IUIAutomationSynchronizedInputPattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_SynchronizedInputPatternId, "SynchronizedInput", AutomationObjectIds.IsSynchronizedInputPatternAvailableProperty);
        public static readonly EventId DiscardedEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_InputDiscardedEventId, "Discarded");
        public static readonly EventId ReachedOtherElementEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_InputReachedOtherElementEventId, "ReachedOtherElement");
        public static readonly EventId ReachedTargetEvent = EventId.GetOrCreate(Interop.UIAutomationClient.UIA_EventIds.UIA_InputReachedTargetEventId, "ReachedTarget");

        public SynchronizedInputPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationSynchronizedInputPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Cancel()
        {
            Com.Call(() => this.NativePattern.Cancel());
        }

        public override void StartListening(SynchronizedInputType inputType)
        {
            Com.Call(() => this.NativePattern.StartListening((Interop.UIAutomationClient.SynchronizedInputType)inputType));
        }
    }
}
