namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ColumnHeader : GridHeader
    {
        public ColumnHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Thumb LeftHeaderGripper
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, new AndCondition(Conditions.ByAutomationId("PART_LeftHeaderGripper"), Conditions.Thumb), Retry.Time, out var result))
                {
                    return (Thumb)result;
                }

                return (Thumb)this.FindFirstChild(Conditions.Thumb);
            }
        }

        public Thumb RightHeaderGripper
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, new AndCondition(Conditions.ByAutomationId("PART_RightHeaderGripper"), Conditions.Thumb), Retry.Time, out var result))
                {
                    return (Thumb)result;
                }

                return (Thumb)this.FindAt(TreeScope.Children, Conditions.Thumb, 1, Retry.Time);
            }
        }
    }
}
