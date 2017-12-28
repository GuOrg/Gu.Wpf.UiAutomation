namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ContextMenu : Menu
    {
        public ContextMenu(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}