namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Globalization;
    using System.Windows.Automation;

    public class Slider : Control
    {
        public Slider(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public Thumb Thumb => this.FindFirstDescendant(ControlType.Thumb, x => new Thumb(x));

        public bool IsOnlyValue => !this.AutomationElement.TryGetRangeValuePattern(out _);

        public double Minimum => this.RangeValuePattern.Current.Minimum;

        public double Maximum => this.RangeValuePattern.Current.Maximum;

        public double SmallChange => this.RangeValuePattern.Current.SmallChange;

        public double LargeChange => this.RangeValuePattern.Current.LargeChange;

        public double Value
        {
            get
            {
                if (this.AutomationElement.TryGetRangeValuePattern(out var rangeValuePattern))
                {
                    return rangeValuePattern.Current.Value;
                }

                // UIA3 for WinForms does not have the RangeValue pattern, only the value pattern
                // The value in this case is always between 0 and 100
                return Convert.ToDouble(this.ValuePattern.Current.Value);
            }

            set
            {
                if (this.AutomationElement.TryGetRangeValuePattern(out var rangeValuePattern))
                {
                    rangeValuePattern.SetValue(value);
                }
                else
                {
                    // UIA3 for WinForms does not have the RangeValue pattern, only the value pattern
                    // The value in this case is always between 0 and 100
                    this.ValuePattern.SetValue(value.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private RangeValuePattern RangeValuePattern => this.AutomationElement.RangeValuePattern();

        private ValuePattern ValuePattern => this.AutomationElement.ValuePattern();

        public void SmallIncrement()
        {
            this.RangeValuePattern.SetValue((this.Value + this.SmallChange).Clamp(this.Minimum, this.Maximum));
        }

        public void SmallDecrement()
        {
            this.RangeValuePattern.SetValue((this.Value - this.SmallChange).Clamp(this.Minimum, this.Maximum));
        }

        public void LargeIncrement()
        {
            this.RangeValuePattern.SetValue((this.Value + this.LargeChange).Clamp(this.Minimum, this.Maximum));
        }

        public void LargeDecrement()
        {
            this.RangeValuePattern.SetValue((this.Value - this.LargeChange).Clamp(this.Minimum, this.Maximum));
        }
    }
}
