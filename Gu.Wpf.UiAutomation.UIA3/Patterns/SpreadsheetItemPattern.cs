namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class SpreadsheetItemPattern : SpreadsheetItemPatternBase<UIA.IUIAutomationSpreadsheetItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_SpreadsheetItemPatternId, "SpreadsheetItem", AutomationObjectIds.IsSpreadsheetItemPatternAvailableProperty);
        public static readonly PropertyId FormulaProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_SpreadsheetItemFormulaPropertyId, "Formula");
        public static readonly PropertyId AnnotationObjectsProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_SpreadsheetItemAnnotationObjectsPropertyId, "AnnotationObjects").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId AnnotationTypesProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_SpreadsheetItemAnnotationTypesPropertyId, "AnnotationTypes").SetConverter((a, o) => AnnotationTypeConverter.ToAnnotationTypeArray(o));

        public SpreadsheetItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationSpreadsheetItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }
    }
}
