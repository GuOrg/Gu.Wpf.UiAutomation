namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class DragPattern : DragPatternBase<UIA.IUIAutomationDragPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_DragPatternId, "Drag", AutomationObjectIds.IsDragPatternAvailableProperty);
        public static readonly PropertyId DropEffectProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_DragDropEffectPropertyId, "DropEffect");
        public static readonly PropertyId DropEffectsProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_DragDropEffectsPropertyId, "DropEffects");
        public static readonly PropertyId IsGrabbedProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_DragIsGrabbedPropertyId, "IsGrabbed");
        public static readonly PropertyId GrabbedItemsProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_DragGrabbedItemsPropertyId, " GrabbedItems").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly EventId DragCancelEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Drag_DragCancelEventId, "DragCancel");
        public static readonly EventId DragCompleteEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Drag_DragCompleteEventId, "DragComplete");
        public static readonly EventId DragStartEvent = EventId.Register(AutomationType.UIA3, UIA.UIA_EventIds.UIA_Drag_DragStartEventId, "DragStart");

        public DragPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationDragPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }
    }
}
