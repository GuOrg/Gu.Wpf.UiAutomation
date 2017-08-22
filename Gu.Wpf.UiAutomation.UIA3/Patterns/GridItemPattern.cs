namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class GridItemPattern : GridItemPatternBase<UIA.IUIAutomationGridItemPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_GridItemPatternId, "GridItem", AutomationObjectIds.IsGridItemPatternAvailableProperty);
        public static readonly PropertyId ColumnProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridItemColumnPropertyId, "Column");
        public static readonly PropertyId ColumnSpanProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridItemColumnSpanPropertyId, "ColumnSpan");
        public static readonly PropertyId ContainingGridProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridItemContainingGridPropertyId, "ContainingGrid").SetConverter(AutomationElementConverter.NativeToManaged);
        public static readonly PropertyId RowProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridItemRowPropertyId, "Row");
        public static readonly PropertyId RowSpanProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridItemRowSpanPropertyId, "RowSpan");

        public GridItemPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationGridItemPattern nativePattern) : base(basicAutomationElement, nativePattern)
        {
        }
    }

    public class GridItemPatternProperties : IGridItemPatternProperties
    {
        public PropertyId Column => GridItemPattern.ColumnProperty;
        public PropertyId ColumnSpan => GridItemPattern.ColumnSpanProperty;
        public PropertyId ContainingGrid => GridItemPattern.ContainingGridProperty;
        public PropertyId Row => GridItemPattern.RowProperty;
        public PropertyId RowSpan => GridItemPattern.RowSpanProperty;
    }
}
