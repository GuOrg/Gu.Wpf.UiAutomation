namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class Calendar : Selector<CalendarDayButton>
    {
        public Calendar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TablePattern TablePattern => this.AutomationElement.TablePattern();

        public GridPattern GridPattern => this.AutomationElement.GridPattern();

        public MultipleViewPattern MultipleViewPattern => this.AutomationElement.MultipleViewPattern();
    }
}