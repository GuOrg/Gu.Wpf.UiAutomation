namespace Gu.Wpf.UiAutomation
{
    public abstract class ScrollBarBase : AutomationElement
    {
        protected ScrollBarBase(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        protected IRangeValuePattern RangeValuePattern => this.Patterns.RangeValue.Pattern;

        protected Button SmallDecrementButton => this.FindButton(this.SmallDecrementText);

        protected Button SmallIncrementButton => this.FindButton(this.SmallIncrementText);

        protected Button LargeDecrementButton => this.FindButton(this.LargeDecrementText);

        protected Button LargeIncrementButton => this.FindButton(this.LargeIncrementText);

        protected Thumb Thumb => this.FindThumb();

        public double Value => this.RangeValuePattern.Value.Value;

        public double MinimumValue => this.RangeValuePattern.Minimum.Value;

        public double MaximumValue => this.RangeValuePattern.Maximum.Value;

        public double SmallChange => this.RangeValuePattern.SmallChange.Value;

        public double LargeChange => this.RangeValuePattern.LargeChange.Value;

        public bool IsReadOnly => this.RangeValuePattern.IsReadOnly.Value;

        protected abstract string SmallDecrementText { get; }

        protected abstract string SmallIncrementText { get; }

        protected abstract string LargeDecrementText { get; }

        protected abstract string LargeIncrementText { get; }

        private Button FindButton(string automationId)
        {
            var button = this.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId(automationId)));
            return button?.AsButton();
        }

        private Thumb FindThumb()
        {
            var thumb = this.FindFirstChild(cf => cf.ByControlType(ControlType.Thumb));
            return thumb?.AsThumb();
        }
    }
}
