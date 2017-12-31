namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ProgressBar : UiElement
    {
        public ProgressBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public RangeValuePattern RangeValuePattern => this.AutomationElement.RangeValuePattern();

        public double Minimum => this.RangeValuePattern.Current.Minimum;

        public double Maximum => this.RangeValuePattern.Current.Maximum;

        public double Value => this.RangeValuePattern.Current.Value;
    }
}
