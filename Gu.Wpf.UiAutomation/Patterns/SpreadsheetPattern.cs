namespace Gu.Wpf.UiAutomation.Patterns
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns.Infrastructure;

    public interface ISpreadsheetPattern : IPattern
    {
        AutomationElement GetItemByName(string name);
    }
}
