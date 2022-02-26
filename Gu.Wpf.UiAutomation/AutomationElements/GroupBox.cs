namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class GroupBox : HeaderedContentControl
    {
        public GroupBox(AutomationElement automationElement)
            : base(automationElement)
        {
        }
    }
}
