namespace Gu.Wpf.UiAutomation.UIA3.Identifiers
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using UIA = Interop.UIAutomationClient;

    public static class AutomationObjectIds
    {
        // Base element properties
        public static readonly PropertyId AcceleratorKeyProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_AcceleratorKeyPropertyId, "AcceleratorKey");
        public static readonly PropertyId AccessKeyProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_AccessKeyPropertyId, "AccessKey");
        public static readonly PropertyId AriaPropertiesProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_AriaPropertiesPropertyId, "AriaProperties");
        public static readonly PropertyId AriaRoleProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_AriaRolePropertyId, "AriaRole");
        public static readonly PropertyId AutomationIdProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_AutomationIdPropertyId, "AutomationId");
        public static readonly PropertyId BoundingRectangleProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_BoundingRectanglePropertyId, "BoundingRectangle").SetConverter((a, o) => ValueConverter.ToRectangle(o));
        public static readonly PropertyId ClassNameProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ClassNamePropertyId, "ClassName");
        public static readonly PropertyId ClickablePointProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ClickablePointPropertyId, "ClickablePoint").SetConverter((a, o) => ValueConverter.ToPoint(o));
        public static readonly PropertyId ControllerForProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ControllerForPropertyId, "ControllerFor").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId ControlTypeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ControlTypePropertyId, "ControlType").SetConverter((a, o) => ControlTypeConverter.ToControlType(o));
        public static readonly PropertyId CultureProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_CulturePropertyId, "Culture").SetConverter((a, o) => ValueConverter.ToCulture(o));
        public static readonly PropertyId DescribedByProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_DescribedByPropertyId, "DescribedBy");
        public static readonly PropertyId FlowsFromProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_FlowsFromPropertyId, "FlowsFrom");
        public static readonly PropertyId FlowsToProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_FlowsToPropertyId, "FlowsTo");
        public static readonly PropertyId FrameworkIdProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_FrameworkIdPropertyId, "FrameworkId");
        public static readonly PropertyId HasKeyboardFocusProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_HasKeyboardFocusPropertyId, "HasKeyboardFocus");
        public static readonly PropertyId HelpTextProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_HelpTextPropertyId, "HelpText");
        public static readonly PropertyId IsContentElementProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsContentElementPropertyId, "IsContentElement");
        public static readonly PropertyId IsControlElementProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsControlElementPropertyId, "IsControlElement");
        public static readonly PropertyId IsDataValidForFormProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsDataValidForFormPropertyId, "IsDataValidForForm");
        public static readonly PropertyId IsEnabledProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsEnabledPropertyId, "IsEnabled");
        public static readonly PropertyId IsKeyboardFocusableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsKeyboardFocusablePropertyId, "IsKeyboardFocusable");
        public static readonly PropertyId IsOffscreenProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsOffscreenPropertyId, "IsOffscreen");
        public static readonly PropertyId IsPasswordProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsPasswordPropertyId, "IsPassword");
        public static readonly PropertyId IsPeripheralProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsPeripheralPropertyId, "IsPeripheral");
        public static readonly PropertyId IsRequiredForFormProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsRequiredForFormPropertyId, "IsRequiredForForm");
        public static readonly PropertyId ItemStatusProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ItemStatusPropertyId, "ItemStatus");
        public static readonly PropertyId ItemTypeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ItemTypePropertyId, "ItemType");
        public static readonly PropertyId LabeledByProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LabeledByPropertyId, "LabeledBy");
        public static readonly PropertyId LiveSettingProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LiveSettingPropertyId, "LiveSetting");
        public static readonly PropertyId LocalizedControlTypeProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, "LocalizedControlType");
        public static readonly PropertyId NameProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_NamePropertyId, "Name");
        public static readonly PropertyId NativeWindowHandleProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_NativeWindowHandlePropertyId, "NativeWindowHandle").SetConverter((a, o) => ValueConverter.IntToIntPtr(o));
        public static readonly PropertyId OptimizeForVisualContentProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_OptimizeForVisualContentPropertyId, "OptimizeForVisualContent");
        public static readonly PropertyId OrientationProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_OrientationPropertyId, "Orientation");
        public static readonly PropertyId ProcessIdProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ProcessIdPropertyId, "ProcessId");
        public static readonly PropertyId ProviderDescriptionProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_ProviderDescriptionPropertyId, "ProviderDescription");
        public static readonly PropertyId RuntimeIdProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_RuntimeIdPropertyId, "RuntimeId");

        // Pattern availability properties
        public static readonly PropertyId IsAnnotationPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsAnnotationPatternAvailablePropertyId, "IsAnnotationPatternAvailable");
        public static readonly PropertyId IsDockPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsDockPatternAvailablePropertyId, "IsDockPatternAvailable");
        public static readonly PropertyId IsDragPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsDragPatternAvailablePropertyId, "IsDragPatternAvailable");
        public static readonly PropertyId IsDropTargetPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsDropTargetPatternAvailablePropertyId, "IsDropTargetPatternAvailable");
        public static readonly PropertyId IsExpandCollapsePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsExpandCollapsePatternAvailablePropertyId, "IsExpandCollapsePatternAvailable");
        public static readonly PropertyId IsGridItemPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsGridItemPatternAvailablePropertyId, "IsGridItemPatternAvailable");
        public static readonly PropertyId IsGridPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsGridPatternAvailablePropertyId, "IsGridPatternAvailable");
        public static readonly PropertyId IsInvokePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsInvokePatternAvailablePropertyId, "IsInvokePatternAvailable");
        public static readonly PropertyId IsItemContainerPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsItemContainerPatternAvailablePropertyId, "IsItemContainerPatternAvailable");
        public static readonly PropertyId IsLegacyIAccessiblePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsLegacyIAccessiblePatternAvailablePropertyId, "IsLegacyIAccessiblePatternAvailable");
        public static readonly PropertyId IsMultipleViewPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsMultipleViewPatternAvailablePropertyId, "IsMultipleViewPatternAvailable");
        public static readonly PropertyId IsObjectModelPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsObjectModelPatternAvailablePropertyId, "IsObjectModelPatternAvailable");
        public static readonly PropertyId IsRangeValuePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsRangeValuePatternAvailablePropertyId, "IsRangeValuePatternAvailable");
        public static readonly PropertyId IsScrollItemPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsScrollItemPatternAvailablePropertyId, "IsScrollItemPatternAvailable");
        public static readonly PropertyId IsScrollPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsScrollPatternAvailablePropertyId, "IsScrollPatternAvailable");
        public static readonly PropertyId IsSelectionItemPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsSelectionItemPatternAvailablePropertyId, "IsSelectionItemPatternAvailable");
        public static readonly PropertyId IsSelectionPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsSelectionPatternAvailablePropertyId, "IsSelectionPatternAvailable");
        public static readonly PropertyId IsSpreadsheetPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsSpreadsheetPatternAvailablePropertyId, "IsSpreadsheetPatternAvailable");
        public static readonly PropertyId IsSpreadsheetItemPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsSpreadsheetItemPatternAvailablePropertyId, "IsSpreadsheetItemPatternAvailable");
        public static readonly PropertyId IsStylesPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsStylesPatternAvailablePropertyId, "IsStylesPatternAvailable");
        public static readonly PropertyId IsSynchronizedInputPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsSynchronizedInputPatternAvailablePropertyId, "IsSynchronizedInputPatternAvailable");
        public static readonly PropertyId IsTableItemPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTableItemPatternAvailablePropertyId, "IsTableItemPatternAvailable");
        public static readonly PropertyId IsTablePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTablePatternAvailablePropertyId, "IsTablePatternAvailable");
        public static readonly PropertyId IsTextChildPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTextChildPatternAvailablePropertyId, "IsTextChildPatternAvailable");
        public static readonly PropertyId IsTextEditPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTextEditPatternAvailablePropertyId, "IsTextEditPatternAvailable");
        public static readonly PropertyId IsTextPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTextPatternAvailablePropertyId, "IsTextPatternAvailable");
        public static readonly PropertyId IsTextPattern2AvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTextPattern2AvailablePropertyId, "IsTextPattern2Available");
        public static readonly PropertyId IsTogglePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTogglePatternAvailablePropertyId, "IsTogglePatternAvailable");
        public static readonly PropertyId IsTransformPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTransformPatternAvailablePropertyId, "IsTransformPatternAvailable");
        public static readonly PropertyId IsTransformPattern2AvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsTransformPattern2AvailablePropertyId, "IsTransformPattern2Available");
        public static readonly PropertyId IsValuePatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsValuePatternAvailablePropertyId, "IsValuePatternAvailable");
        public static readonly PropertyId IsVirtualizedItemPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsVirtualizedItemPatternAvailablePropertyId, "IsVirtualizedItemPatternAvailable");
        public static readonly PropertyId IsWindowPatternAvailableProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_IsWindowPatternAvailablePropertyId, "IsWindowPatternAvailable");

        public static readonly EventId AsyncContentLoadedEvent = EventId.Register(UIA.UIA_EventIds.UIA_AsyncContentLoadedEventId, "AsyncContentLoaded");
        public static readonly EventId FocusChangedEvent = EventId.Register(UIA.UIA_EventIds.UIA_AutomationFocusChangedEventId, "AutomationFocusChanged");
        public static readonly EventId PropertyChangedEvent = EventId.Register(UIA.UIA_EventIds.UIA_AutomationPropertyChangedEventId, "AutomationPropertyChanged");
        public static readonly EventId HostedFragmentRootsInvalidatedEvent = EventId.Register(UIA.UIA_EventIds.UIA_HostedFragmentRootsInvalidatedEventId, "HostedFragmentRootsInvalidated");
        public static readonly EventId LayoutInvalidatedEvent = EventId.Register(UIA.UIA_EventIds.UIA_LayoutInvalidatedEventId, "LayoutInvalidated");
        public static readonly EventId LiveRegionChangedEvent = EventId.Register(UIA.UIA_EventIds.UIA_LiveRegionChangedEventId, "LiveRegionChanged");
        public static readonly EventId MenuClosedEvent = EventId.Register(UIA.UIA_EventIds.UIA_MenuClosedEventId, "MenuClosed");
        public static readonly EventId MenuModeEndEvent = EventId.Register(UIA.UIA_EventIds.UIA_MenuModeEndEventId, "MenuModeEnd");
        public static readonly EventId MenuModeStartEvent = EventId.Register(UIA.UIA_EventIds.UIA_MenuModeStartEventId, "MenuModeStart");
        public static readonly EventId MenuOpenedEvent = EventId.Register(UIA.UIA_EventIds.UIA_MenuOpenedEventId, "MenuOpened");
        public static readonly EventId StructureChangedEvent = EventId.Register(UIA.UIA_EventIds.UIA_StructureChangedEventId, "StructureChanged");
        public static readonly EventId SystemAlertEvent = EventId.Register(UIA.UIA_EventIds.UIA_SystemAlertEventId, "SystemAlert");
        public static readonly EventId ToolTipClosedEvent = EventId.Register(UIA.UIA_EventIds.UIA_ToolTipClosedEventId, "ToolTipClosed");
        public static readonly EventId ToolTipOpenedEvent = EventId.Register(UIA.UIA_EventIds.UIA_ToolTipOpenedEventId, "ToolTipOpened");
    }
}
