namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class RowHeader : GridHeader
    {
        public RowHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Thumb TopHeaderGripper
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, new AndCondition(Conditions.ByAutomationId("PART_TopHeaderGripper"), Conditions.Thumb), Retry.Time, out var result))
                {
                    return (Thumb)result;
                }

                return (Thumb)this.FindFirstChild(Conditions.Thumb);
            }
        }

        public Thumb BottomHeaderGripper
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, new AndCondition(Conditions.ByAutomationId("PART_BottomHeaderGripper"), Conditions.Thumb), Retry.Time, out var result))
                {
                    return (Thumb)result;
                }

                return (Thumb)this.FindAt(TreeScope.Children, Conditions.Thumb, 1, Retry.Time);
            }
        }
    }
}
