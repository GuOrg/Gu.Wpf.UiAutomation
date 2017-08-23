namespace Gu.Wpf.UiAutomation.UIA2.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA2.Identifiers;
    using UIA = System.Windows.Automation;

    public class InvokePattern : InvokePatternBase<UIA.InvokePattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA2, UIA.InvokePattern.Pattern.Id, "Invoke", AutomationObjectIds.IsInvokePatternAvailableProperty);
        public static readonly EventId InvokedEvent = EventId.Register(AutomationType.UIA2, UIA.InvokePattern.InvokedEvent.Id, "Invoked");

        public InvokePattern(BasicAutomationElementBase basicAutomationElement, UIA.InvokePattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Invoke()
        {
            this.NativePattern.Invoke();
        }
    }
}
