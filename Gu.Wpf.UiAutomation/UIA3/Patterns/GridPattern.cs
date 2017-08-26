namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class GridPattern : GridPatternBase<Interop.UIAutomationClient.IUIAutomationGridPattern>
    {
        public static readonly PatternId Pattern = PatternId.Register(Interop.UIAutomationClient.UIA_PatternIds.UIA_GridPatternId, "Grid", AutomationObjectIds.IsGridPatternAvailableProperty);
        public static readonly PropertyId ColumnCountProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridColumnCountPropertyId, "ColumnCount");
        public static readonly PropertyId RowCountProperty = PropertyId.Register(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridRowCountPropertyId, "RowCount");

        public GridPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationGridPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override AutomationElement GetItem(int row, int column)
        {
            var nativeItem = ComCallWrapper.Call(() => this.NativePattern.GetItem(row, column));
            return AutomationElementConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeItem);
        }
    }
}
