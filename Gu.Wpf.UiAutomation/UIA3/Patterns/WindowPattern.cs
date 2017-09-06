namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class WindowPattern : WindowPatternBase<Interop.UIAutomationClient.IUIAutomationWindowPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_WindowPatternId, "Window", AutomationObjectIds.IsWindowPatternAvailableProperty);
        public static readonly PropertyId CanMaximizeProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_WindowCanMaximizePropertyId, "CanMaximize");
        public static readonly PropertyId CanMinimizeProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_WindowCanMinimizePropertyId, "CanMinimize");
        public static readonly PropertyId IsModalProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_WindowIsModalPropertyId, "IsModal");
        public static readonly PropertyId IsTopmostProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_WindowIsTopmostPropertyId, "IsTopmost");
        public static readonly PropertyId WindowInteractionStateProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_WindowWindowInteractionStatePropertyId, "WindowInteractionState");
        public static readonly PropertyId WindowVisualStateProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_WindowWindowVisualStatePropertyId, "WindowVisualState");
        public static readonly EventId WindowClosedEvent = EventId.Register(Interop.UIAutomationClient.UIA_EventIds.UIA_Window_WindowClosedEventId, "WindowClosed");
        public static readonly EventId WindowOpenedEvent = EventId.Register(Interop.UIAutomationClient.UIA_EventIds.UIA_Window_WindowOpenedEventId, "WindowOpened");

        public WindowPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationWindowPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Close()
        {
            ComCallWrapper.Call(() => this.NativePattern.Close());
        }

        public override void SetWindowVisualState(WindowVisualState state)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetWindowVisualState((Interop.UIAutomationClient.WindowVisualState)state));
        }

        public override bool WaitForInputIdle(int milliseconds)
        {
            return ComCallWrapper.Call(() => this.NativePattern.WaitForInputIdle(milliseconds)) != 0;
        }
    }
}
