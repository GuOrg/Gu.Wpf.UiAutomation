namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Globalization;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Input;
    using Gu.Wpf.UiAutomation.Patterns;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    public class Slider : AutomationElement
    {
        public Slider(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        private IRangeValuePattern RangeValuePattern => this.Patterns.RangeValue.PatternOrDefault;

        private IValuePattern ValuePattern => this.Patterns.Value.PatternOrDefault;

        private Button LargeIncreaseButton => this.GetLargeIncreaseButton();

        private Button LargeDecreaseButton => this.GetLargeDecreaseButton();

        public Thumb Thumb => this.FindFirstChild(cf => cf.ByControlType(ControlType.Thumb))?.AsThumb();

        public bool IsOnlyValue => !this.IsPatternSupported(this.Automation.PatternLibrary.RangeValuePattern);

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

        public void SmallIncrement()
        {
            Keyboard.Press(VirtualKeyShort.RIGHT);
            Helpers.WaitUntilInputIsProcessed();
        }

        public void SmallDecrement()
        {
            Keyboard.Press(VirtualKeyShort.LEFT);
            Helpers.WaitUntilInputIsProcessed();
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
                return this.FindFirstChild(cf => cf.ByAutomationId("IncreaseLarge")).AsButton();
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
                return this.FindFirstChild(cf => cf.ByAutomationId("DecreaseLarge")).AsButton();
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
