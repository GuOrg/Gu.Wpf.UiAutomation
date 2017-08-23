namespace Gu.Wpf.UiAutomation.UIA3.Patterns
{
    using Gu.Wpf.UiAutomation.Identifiers;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UIA3.Converters;
    using Gu.Wpf.UiAutomation.UIA3.Identifiers;
    using UIA = Interop.UIAutomationClient;

    public class SpreadsheetPattern : PatternBase<UIA.IUIAutomationSpreadsheetPattern>, ISpreadsheetPattern
    {
        public static readonly PatternId Pattern = PatternId.Register(UIA.UIA_PatternIds.UIA_SpreadsheetPatternId, "Spreadsheet", AutomationObjectIds.IsSpreadsheetPatternAvailableProperty);

        public SpreadsheetPattern(BasicAutomationElementBase basicAutomationElement, UIA.IUIAutomationSpreadsheetPattern nativePattern)
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
