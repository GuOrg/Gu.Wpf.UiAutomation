namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Patterns;

    public class ProgressBar : AutomationElement
    {
        public ProgressBar(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        public IRangeValuePattern RangeValuePattern => Patterns.RangeValue.Pattern;

        public double Minimum => RangeValuePattern.Minimum.Value;

        public double Maximum => RangeValuePattern.Maximum.Value;

        public double Value => RangeValuePattern.Value.Value;
    }
}
