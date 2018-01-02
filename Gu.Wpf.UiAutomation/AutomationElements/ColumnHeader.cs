namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ColumnHeader : GridHeader
    {
        public ColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Thumb LeftHeaderGripper => (Thumb)this.FindFirstChild(Condition.Thumb);

        public Thumb RightHeaderGripper => (Thumb)this.FindAt(TreeScope.Children, Condition.Thumb, 1, Retry.Time);
    }
}
