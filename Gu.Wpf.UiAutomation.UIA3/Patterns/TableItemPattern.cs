namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class TableItemPattern : TableItemPatternBase<UIA.IUIAutomationTableItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_TableItemPatternId, "TableItem", AutomationObjectIds.IsTableItemPatternAvailableProperty);
        public static readonly PropertyId ColumnHeaderItemsProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_TableItemColumnHeaderItemsPropertyId, "ColumnHeaderItems").SetConverter(AutomationElementConverter.NativeArrayToManaged);
        public static readonly PropertyId RowHeaderItemsProperty = PropertyId.Register(UIA.UIA_PropertyIds.UIA_TableItemRowHeaderItemsPropertyId, "RowHeaderItems").SetConverter(AutomationElementConverter.NativeArrayToManaged);

        public TableItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationTableItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }
    }
}
