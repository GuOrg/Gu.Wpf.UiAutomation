﻿namespace Gu.Wpf.UiAutomation.UIA3
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class UIA3AutomationElementPatternAvailabilityProperties : IAutomationElementPatternAvailabilityProperties
    {
        public PropertyId IsAnnotationPatternAvailable => AutomationObjectIds.IsAnnotationPatternAvailableProperty;

        public PropertyId IsDockPatternAvailable => AutomationObjectIds.IsDockPatternAvailableProperty;

        public PropertyId IsDragPatternAvailable => AutomationObjectIds.IsDragPatternAvailableProperty;

        public PropertyId IsDropTargetPatternAvailable => AutomationObjectIds.IsDropTargetPatternAvailableProperty;

        public PropertyId IsExpandCollapsePatternAvailable => AutomationObjectIds.IsExpandCollapsePatternAvailableProperty;

        public PropertyId IsGridItemPatternAvailable => AutomationObjectIds.IsGridItemPatternAvailableProperty;

        public PropertyId IsGridPatternAvailable => AutomationObjectIds.IsGridPatternAvailableProperty;

        public PropertyId IsInvokePatternAvailable => AutomationObjectIds.IsInvokePatternAvailableProperty;

        public PropertyId IsItemContainerPatternAvailable => AutomationObjectIds.IsItemContainerPatternAvailableProperty;

        public PropertyId IsLegacyIAccessiblePatternAvailable => AutomationObjectIds.IsLegacyIAccessiblePatternAvailableProperty;

        public PropertyId IsMultipleViewPatternAvailable => AutomationObjectIds.IsMultipleViewPatternAvailableProperty;

        public PropertyId IsObjectModelPatternAvailable => AutomationObjectIds.IsObjectModelPatternAvailableProperty;

        public PropertyId IsRangeValuePatternAvailable => AutomationObjectIds.IsRangeValuePatternAvailableProperty;

        public PropertyId IsScrollItemPatternAvailable => AutomationObjectIds.IsScrollItemPatternAvailableProperty;

        public PropertyId IsScrollPatternAvailable => AutomationObjectIds.IsScrollPatternAvailableProperty;

        public PropertyId IsSelectionItemPatternAvailable => AutomationObjectIds.IsSelectionItemPatternAvailableProperty;

        public PropertyId IsSelectionPatternAvailable => AutomationObjectIds.IsSelectionPatternAvailableProperty;

        public PropertyId IsSpreadsheetPatternAvailable => AutomationObjectIds.IsSpreadsheetPatternAvailableProperty;

        public PropertyId IsSpreadsheetItemPatternAvailable => AutomationObjectIds.IsSpreadsheetItemPatternAvailableProperty;

        public PropertyId IsStylesPatternAvailable => AutomationObjectIds.IsStylesPatternAvailableProperty;

        public PropertyId IsSynchronizedInputPatternAvailable => AutomationObjectIds.IsSynchronizedInputPatternAvailableProperty;

        public PropertyId IsTableItemPatternAvailable => AutomationObjectIds.IsTableItemPatternAvailableProperty;

        public PropertyId IsTablePatternAvailable => AutomationObjectIds.IsTablePatternAvailableProperty;

        public PropertyId IsTextChildPatternAvailable => AutomationObjectIds.IsTextChildPatternAvailableProperty;

        public PropertyId IsTextEditPatternAvailable => AutomationObjectIds.IsTextEditPatternAvailableProperty;

        public PropertyId IsTextPatternAvailable => AutomationObjectIds.IsTextPatternAvailableProperty;

        public PropertyId IsTextPattern2Available => AutomationObjectIds.IsTextPattern2AvailableProperty;

        public PropertyId IsTogglePatternAvailable => AutomationObjectIds.IsTogglePatternAvailableProperty;

        public PropertyId IsTransformPatternAvailable => AutomationObjectIds.IsTransformPatternAvailableProperty;

        public PropertyId IsTransformPattern2Available => AutomationObjectIds.IsTransformPattern2AvailableProperty;

        public PropertyId IsValuePatternAvailable => AutomationObjectIds.IsValuePatternAvailableProperty;

        public PropertyId IsVirtualizedItemPatternAvailable => AutomationObjectIds.IsVirtualizedItemPatternAvailableProperty;

        public PropertyId IsWindowPatternAvailable => AutomationObjectIds.IsWindowPatternAvailableProperty;

        public PropertyId[] AllForCurrentFramework => new[]
        {
            this.IsAnnotationPatternAvailable,
            this.IsDockPatternAvailable,
            this.IsDragPatternAvailable,
            this.IsDropTargetPatternAvailable,
            this.IsExpandCollapsePatternAvailable,
            this.IsGridItemPatternAvailable,
            this.IsGridPatternAvailable,
            this.IsInvokePatternAvailable,
            this.IsItemContainerPatternAvailable,
            this.IsLegacyIAccessiblePatternAvailable,
            this.IsMultipleViewPatternAvailable,
            this.IsObjectModelPatternAvailable,
            this.IsRangeValuePatternAvailable,
            this.IsScrollItemPatternAvailable,
            this.IsScrollPatternAvailable,
            this.IsSelectionItemPatternAvailable,
            this.IsSelectionPatternAvailable,
            this.IsSpreadsheetPatternAvailable,
            this.IsSpreadsheetItemPatternAvailable,
            this.IsStylesPatternAvailable,
            this.IsSynchronizedInputPatternAvailable,
            this.IsTableItemPatternAvailable,
            this.IsTablePatternAvailable,
            this.IsTextChildPatternAvailable,
            this.IsTextEditPatternAvailable,
            this.IsTextPatternAvailable,
            this.IsTextPattern2Available,
            this.IsTogglePatternAvailable,
            this.IsTransformPatternAvailable,
            this.IsTransformPattern2Available,
            this.IsValuePatternAvailable,
            this.IsVirtualizedItemPatternAvailable,
            this.IsWindowPatternAvailable
        };
    }
}