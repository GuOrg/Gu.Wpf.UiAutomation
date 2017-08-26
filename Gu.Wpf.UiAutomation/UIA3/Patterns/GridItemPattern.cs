namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class GridItemPattern : GridItemPatternBase<Interop.UIAutomationClient.IUIAutomationGridItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_GridItemPatternId, "GridItem", AutomationObjectIds.IsGridItemPatternAvailableProperty);
        public static readonly PropertyId ColumnProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridItemColumnPropertyId, "Column");
        public static readonly PropertyId ColumnSpanProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridItemColumnSpanPropertyId, "ColumnSpan");
        public static readonly PropertyId ContainingGridProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridItemContainingGridPropertyId, "ContainingGrid").SetConverter(AutomationElementConverter.NativeToManaged);
        public static readonly PropertyId RowProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridItemRowPropertyId, "Row");
        public static readonly PropertyId RowSpanProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridItemRowSpanPropertyId, "RowSpan");

        public GridItemPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationGridItemPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }
    }
}
