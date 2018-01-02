namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class RowHeader : GridHeader
    {
        public RowHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Thumb TopHeaderGripper => (Thumb)this.FindFirstChild(Condition.Thumb);

        public Thumb BottomHeaderGripper => (Thumb)this.FindAt(TreeScope.Children, Condition.Thumb, 1, Retry.Time);
    }
}
