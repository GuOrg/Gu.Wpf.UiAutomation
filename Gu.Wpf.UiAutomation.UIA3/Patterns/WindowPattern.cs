namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class WindowPattern : WindowPatternBase<UIA.IUIAutomationWindowPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_WindowPatternId, "Window", AutomationObjectIds.IsWindowPatternAvailableProperty);
        public static readonly PropertyId CanMaximizeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_WindowCanMaximizePropertyId, "CanMaximize");
        public static readonly PropertyId CanMinimizeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_WindowCanMinimizePropertyId, "CanMinimize");
        public static readonly PropertyId IsModalProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_WindowIsModalPropertyId, "IsModal");
        public static readonly PropertyId IsTopmostProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_WindowIsTopmostPropertyId, "IsTopmost");
        public static readonly PropertyId WindowInteractionStateProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_WindowWindowInteractionStatePropertyId, "WindowInteractionState");
        public static readonly PropertyId WindowVisualStateProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_WindowWindowVisualStatePropertyId, "WindowVisualState");
        public static readonly EventId WindowClosedEvent = EventId.Register(UIA.UIA_EventIds.UIA_Window_WindowClosedEventId, "WindowClosed");
        public static readonly EventId WindowOpenedEvent = EventId.Register(UIA.UIA_EventIds.UIA_Window_WindowOpenedEventId, "WindowOpened");

        public WindowPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationWindowPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Close()
        {
            ComCallWrapper.Call(() => this.NativePattern.Close());
        }

        public override void SetWindowVisualState(WindowVisualState state)
        {
            ComCallWrapper.Call(() => this.NativePattern.SetWindowVisualState((UIA.WindowVisualState)state));
        }

        public override bool WaitForInputIdle(int milliseconds)
        {
            return ComCallWrapper.Call(() => this.NativePattern.WaitForInputIdle(milliseconds)) != 0;
        }
    }
}
