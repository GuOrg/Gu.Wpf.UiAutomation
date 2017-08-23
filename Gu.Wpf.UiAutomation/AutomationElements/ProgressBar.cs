namespace Gu.Wpf.UiAutomation
{
    using Gu.Wpf.UiAutomation.Patterns;

    public class ProgressBar : AutomationElement
    {
        public ProgressBar(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public IRangeValuePattern RangeValuePattern => this.Patterns.RangeValue.Pattern;

        public double Minimum => this.RangeValuePattern.Minimum.Value;

        public double Maximum => this.RangeValuePattern.Maximum.Value;

        public double Value => this.RangeValuePattern.Value.Value;
    }
}
