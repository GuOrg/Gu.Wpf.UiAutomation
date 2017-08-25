namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Globalization;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class Slider : Control
    {
        public Slider(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public Thumb Thumb => this.FindFirstChild(cf => cf.ByControlType(ControlType.Thumb))?.AsThumb();

        public bool IsOnlyValue => !this.IsPatternSupported(this.Automation.PatternLibrary.RangeValuePattern);

        public double Minimum => this.Patterns.RangeValue.Pattern.Minimum;

        public double Maximum => this.Patterns.RangeValue.Pattern.Maximum;

        public double LargeChange => this.Patterns.RangeValue.Pattern.LargeChange;

        public double Value
        {
            get
            {
                var rangeValuePattern = this.RangeValuePattern;
                if (rangeValuePattern != null)
                {
                    return this.RangeValuePattern.Value.Value;
                }

                // UIA3 for WinForms does not have the RangeValue pattern, only the value pattern
                // The value in this case is always between 0 and 100
                return Convert.ToDouble(this.ValuePattern.Value.Value);
            }

            set
            {
                var rangeValuePattern = this.RangeValuePattern;
                if (rangeValuePattern != null)
                {
                    this.RangeValuePattern.SetValue(value);
                }
                else
                {
                    // UIA3 for WinForms does not have the RangeValue pattern, only the value pattern
                    // The value in this case is always between 0 and 100
                    this.ValuePattern.SetValue(value.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private IRangeValuePattern RangeValuePattern => this.Patterns.RangeValue.PatternOrDefault;

        private IValuePattern ValuePattern => this.Patterns.Value.PatternOrDefault;

        private Button LargeIncreaseButton => this.GetLargeIncreaseButton();

        private Button LargeDecreaseButton => this.GetLargeDecreaseButton();

        public void SmallIncrement()
        {
            using (Keyboard.Pressing(VirtualKeyShort.RIGHT))
            {
                Wait.UntilInputIsProcessed();
            }
        }

        public void SmallDecrement()
        {
            using (Keyboard.Pressing(VirtualKeyShort.LEFT))
            {
                Wait.UntilInputIsProcessed();
            }
        }

        public void LargeIncrement()
        {
            this.LargeIncreaseButton.Invoke();
        }

        public void LargeDecrement()
        {
            this.LargeDecreaseButton.Invoke();
        }

        private Button GetLargeIncreaseButton()
        {
            if (this.FrameworkType == FrameworkType.Wpf)
            {
                // For WPF, this is simple
                return this.FindButton("IncreaseLarge");
            }

            // For WinForms, we loop thru the buttons and find the one right of the thumb
            var buttons = this.FindAllChildren(cf => cf.ByControlType(ControlType.Button));
            foreach (var button in buttons)
            {
                if (button.Properties.BoundingRectangle.Value.Left > this.Thumb.Properties.BoundingRectangle.Value.Left)
                {
                    return button.AsButton();
                }
            }

            return null;
        }

        private Button GetLargeDecreaseButton()
        {
            if (this.FrameworkType == FrameworkType.Wpf)
            {
                // For WPF, this is simple
                return this.FindButton("DecreaseLarge");
            }

            // For WinForms, we loop thru the buttons and find the one left of the thumb
            var buttons = this.FindAllChildren(cf => cf.ByControlType(ControlType.Button));
            foreach (var button in buttons)
            {
                if (button.Properties.BoundingRectangle.Value.Right < this.Thumb.Properties.BoundingRectangle.Value.Right)
                {
                    return button.AsButton();
                }
            }

            return null;
        }
    }
}
