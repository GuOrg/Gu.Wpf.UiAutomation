namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class InvokePattern : InvokePatternBase<UIA.IUIAutomationInvokePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_InvokePatternId, "Invoke", AutomationObjectIds.IsInvokePatternAvailableProperty);
        public static readonly EventId InvokedEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Invoke_InvokedEventId, "Invoked");

        public InvokePattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationInvokePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Invoke()
        {
            ComCallWrapper.Call(() => this.NativePattern.Invoke());
        }
    }

    public class InvokePatternEvents : IInvokePatternEvents
    {
        public EventId InvokedEvent => InvokePattern.InvokedEvent;
    }
}
