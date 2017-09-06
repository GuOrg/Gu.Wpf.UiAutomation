namespace Gu.Wpf.UiAutomation
{
    public interface ISpreadsheetPattern : IPattern
    {
        AutomationElement GetItemByName(string name);
    }
}
