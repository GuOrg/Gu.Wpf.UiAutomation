namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ListView : GridView
    {
        public ListView(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}