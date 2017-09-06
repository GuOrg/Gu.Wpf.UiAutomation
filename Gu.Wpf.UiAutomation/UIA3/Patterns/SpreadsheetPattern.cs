namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;

    public class SpreadsheetPattern : PatternBase<Interop.UIAutomationClient.IUIAutomationSpreadsheetPattern>, ISpreadsheetPattern
    {
        public static readonly PatternId Pattern = PatternId.GetOrCreate(Interop.UIAutomationClient.UIA_PatternIds.UIA_SpreadsheetPatternId, "Spreadsheet", AutomationObjectIds.IsSpreadsheetPatternAvailableProperty);

        public SpreadsheetPattern(BasicAutomationElementBase basicAutomationElement, Interop.UIAutomationClient.IUIAutomationSpreadsheetPattern nativePattern)
            : base(basicAutomationElement, nativePattern)
        {
        }

        public AutomationElement GetItemByName(string name)
        {
            var nativeElement = ComCallWrapper.Call(() => this.NativePattern.GetItemByName(name));
            return AutomationElementConverter.NativeToManaged((UIA3Automation)this.BasicAutomationElement.Automation, nativeElement);
        }
    }
}
