namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ProgressBar : UiElement
    {
        public ProgressBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public IRangeValuePattern RangeValuePattern => this.Patterns.RangeValue.Pattern;

        public double Minimum => this.RangeValuePattern.Minimum.Value;

        public double Maximum => this.RangeValuePattern.Maximum.Value;

        public double Value => this.RangeValuePattern.Value.Value;
    }
}
