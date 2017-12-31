namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public abstract class ScrollBarBase : UiElement
    {
        protected ScrollBarBase(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public double Value => this.RangeValuePattern.Current.Value;

        public double MinimumValue => this.RangeValuePattern.Current.Minimum;

        public double MaximumValue => this.RangeValuePattern.Current.Maximum;

        public double SmallChange => this.RangeValuePattern.Current.SmallChange;

        public double LargeChange => this.RangeValuePattern.Current.LargeChange;

        public bool IsReadOnly => this.RangeValuePattern.Current.IsReadOnly;

        protected abstract string SmallDecrementText { get; }

        protected abstract string SmallIncrementText { get; }

        protected abstract string LargeDecrementText { get; }

        protected abstract string LargeIncrementText { get; }

        protected RangeValuePattern RangeValuePattern => this.AutomationElement.RangeValuePattern();

        protected Button SmallDecrementButton => this.FindButton(this.SmallDecrementText);

        protected Button SmallIncrementButton => this.FindButton(this.SmallIncrementText);

        protected Button LargeDecrementButton => this.FindButton(this.LargeDecrementText);

        protected Button LargeIncrementButton => this.FindButton(this.LargeIncrementText);

        protected Thumb Thumb => this.FindThumb();

        private Thumb FindThumb()
        {
            var thumb = this.FindFirstChild(Condition.Thumb);
            return thumb?.AsThumb();
        }
    }
}
