namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using System;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class GridPattern : GridPatternBase<Interop.UIAutomationClient.IUIAutomationGridPattern>
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_GridPatternId, "Grid", AutomationObjectIds.IsGridPatternAvailableProperty);
        public static readonly PropertyId ColumnCountProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridColumnCountPropertyId, "ColumnCount");
        public static readonly PropertyId RowCountProperty = PropertyId.GetOrCreate(Interop.UIAutomationClient.UIA_PropertyIds.UIA_GridRowCountPropertyId, "RowCount");

        public GridPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationGridPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public override AutomationElement GetItem(int row, int column)
        {
            var nativeItem = Com.Call(() => this.NativePattern.GetItem(row, column));
            return AutomationElementConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeItem);
        }

        public override T GetItem<T>(int row, int column, Func<BasicAutomationElementBase, T> wrap)
        {
            var nativeItem = Com.Call(() => this.NativePattern.GetItem(row, column));
            if (nativeItem == null)
            {
                throw new InvalidOperationException("Did not find item.");
            }

            return wrap(new UIA3BasicAutomationElement((UIA3Automation)this.BasicAutomationElement.Automation, nativeItem));
        }
    }
}
