namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class GridPattern : GridPatternBase<UIA.IUIAutomationGridPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(AutomationType.UIA3, UIA.UIA_PatternIds.UIA_GridPatternId, "Grid", AutomationObjectIds.IsGridPatternAvailableProperty);
        public static readonly PropertyId ColumnCountProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridColumnCountPropertyId, "ColumnCount");
        public static readonly PropertyId RowCountProperty = PropertyId.Register(AutomationType.UIA3, UIA.UIA_PropertyIds.UIA_GridRowCountPropertyId, "RowCount");

        public GridPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationGridPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override AutomationElement GetItem(int row, int column)
        {
            var nativeItem = ComCallWrapper.Call(() => this.NativePattern.GetItem(row, column));
            return AutomationElementConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeItem);
        }
    }

    public class GridPatternProperties : IGridPatternProperties
    {
        public PropertyId ColumnCount => GridPattern.ColumnCountProperty;

        public PropertyId RowCount => GridPattern.RowCountProperty;
    }
}
