namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ContextMenu : Menu
    {
        public ContextMenu(AutomationElement automationElement, bool isWin32Menu = false)
            : base(automationElement, isWin32Menu)
        {
        }
    }
}