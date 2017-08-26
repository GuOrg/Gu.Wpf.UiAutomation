namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class SpreadsheetItemPattern : SpreadsheetItemPatternBase<Interop.UIAutomationClient.IUIAutomationSpreadsheetItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_SpreadsheetItemPatternId, "SpreadsheetItem", AutomationObjectIds.IsSpreadsheetItemPatternAvailableProperty);
        public static readonly PropertyId FormulaProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_SpreadsheetItemFormulaPropertyId, "Formula");
        public static readonly PropertyId AnnotationObjectsProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_SpreadsheetItemAnnotationObjectsPropertyId, "AnnotationObjects").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId AnnotationTypesProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_SpreadsheetItemAnnotationTypesPropertyId, "AnnotationTypes").SetConverter((a, o) => AnnotationTypeConverter.ToAnnotationTypeArray(o));

        public SpreadsheetItemPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationSpreadsheetItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }
    }
}
