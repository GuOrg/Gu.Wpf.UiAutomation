namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.Windows;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class SliderTests : UITestBase
    {
        public SliderTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void SlideThumbTest()
        {
            var slider = this.GetSlider();
            var thumb = slider.Thumb;
            var oldPos = thumb.Properties.BoundingRectangle.Value.Center();
            thumb.SlideHorizontally(50);
            Helpers.WaitUntilInputIsProcessed();
            TestUtilities.AssertPointsAreSame(thumb.Properties.BoundingRectangle.Value.Center(), new Point(oldPos.X + 50, oldPos.Y), 1);
        }

        [Test]
        public void SetValueTest()
        {
            var slider = this.GetSlider();
            var number1 = this.AdjustNumberIfOnlyValue(slider, 6);
            slider.Value = number1;
            Assert.That(slider.Value, Is.EqualTo(number1));
            var number2 = this.AdjustNumberIfOnlyValue(slider, 4);
            slider.Value = number2;
            Assert.That(slider.Value, Is.EqualTo(number2));
        }

        [Test]
        public void SmallIncrementTest()
        {
            var slider = this.GetSlider();
            this.ResetToCenter(slider);
            slider.SmallIncrement();
            Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 6)));
        }

        [Test]
        public void SmallDecrementTest()
        {
            var slider = this.GetSlider();
            this.ResetToCenter(slider);
            slider.SmallDecrement();
            Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 4)));
        }

        [Test]
        public void LargeIncrementTest()
        {
            var slider = this.GetSlider();
            this.ResetToCenter(slider);
            slider.LargeIncrement();
            Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 9)));
        }

        [Test]
        public void LargeDecrementTest()
        {
            var slider = this.GetSlider();
            this.ResetToCenter(slider);
            slider.LargeDecrement();
            Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 1)));
        }

        private Slider GetSlider()
        {
            var element = this.App.MainWindow().FindFirstDescendant(cf => cf.ByAutomationId("Slider")).AsSlider();
            return element;
        }

        /// <summary>
        /// The range of the test slider is set to 0-10, but in UIA3 WinForms,
        /// the range is always 0-100, so we fix this here
        /// </summary>
        private double AdjustNumberIfOnlyValue(Slider slider, double number)
        {
            if (slider.IsOnlyValue)
            {
                return number * 10;
            }

            return number;
        }

        private void ResetToCenter(Slider slider)
        {
            slider.Value = this.AdjustNumberIfOnlyValue(slider, 5);
        }
    }
}
