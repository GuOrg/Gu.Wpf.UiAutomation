namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class WindowPattern : WindowPatternBase<UIA.IUIAutomationWindowPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_WindowPatternId, "Window", AutomationObjectIds.IsWindowPatternAvailableProperty);
        public static readonly PropertyId CanMaximizeProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_WindowCanMaximizePropertyId, "CanMaximize");
        public static readonly PropertyId CanMinimizeProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_WindowCanMinimizePropertyId, "CanMinimize");
        public static readonly PropertyId IsModalProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_WindowIsModalPropertyId, "IsModal");
        public static readonly PropertyId IsTopmostProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_WindowIsTopmostPropertyId, "IsTopmost");
        public static readonly PropertyId WindowInteractionStateProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_WindowWindowInteractionStatePropertyId, "WindowInteractionState");
        public static readonly PropertyId WindowVisualStateProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_WindowWindowVisualStatePropertyId, "WindowVisualState");
        public static readonly EventId WindowClosedEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Window_WindowClosedEventId, "WindowClosed");
        public static readonly EventId WindowOpenedEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Window_WindowOpenedEventId, "WindowOpened");

        public WindowPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationWindowPattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }

        public override void Close()
        {
            ComCallWrapper.Call(() => NativePattern.Close());
        }

        public override void SetWindowVisualState(WindowVisualState state)
        {
            ComCallWrapper.Call(() => NativePattern.SetWindowVisualState((UIA.WindowVisualState)state));
        }

        public override bool WaitForInputIdle(int milliseconds)
        {
            return ComCallWrapper.Call(() => NativePattern.WaitForInputIdle(milliseconds)) != 0;
        }
    }

    public class WindowPatternProperties : IWindowPatternProperties
    {
        public PropertyId CanMaximize => WindowPattern.CanMaximizeProperty;

        public PropertyId CanMinimize => WindowPattern.CanMinimizeProperty;

        public PropertyId IsModal => WindowPattern.IsModalProperty;

        public PropertyId IsTopmost => WindowPattern.IsTopmostProperty;

        public PropertyId WindowInteractionState => WindowPattern.WindowInteractionStateProperty;

        public PropertyId WindowVisualState => WindowPattern.WindowVisualStateProperty;
    }

    public class WindowPatternEvents : IWindowPatternEvents
    {
        public EventId WindowClosedEvent => WindowPattern.WindowClosedEvent;

        public EventId WindowOpenedEvent => WindowPattern.WindowOpenedEvent;
    }
}
