namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ColumnHeader : GridHeader
    {
        public ColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Thumb LeftHeaderGripper => (Thumb)this.FindFirstChild(Conditions.Thumb);

        public Thumb RightHeaderGripper => (Thumb)this.FindAt(TreeScope.Children, Conditions.Thumb, 1, Retry.Time);
    }
}
