namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class SliderTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Properties()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                Assert.AreEqual(0, slider.Minimum);
                Assert.AreEqual(10, slider.Maximum);
                Assert.AreEqual(5, slider.Value);
                Assert.AreEqual(4, slider.LargeChange);
            }
        }

        [Test]
        public void SlideThumbTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                var thumb = slider.Thumb;
                var oldPos = thumb.Properties.BoundingRectangle.Value.Center();
                thumb.SlideHorizontally(50);
                Wait.UntilInputIsProcessed();

                Assert.AreEqual(oldPos.X + 49, thumb.Properties.BoundingRectangle.Value.Center().X);
                Assert.AreEqual(oldPos.Y, thumb.Properties.BoundingRectangle.Value.Center().Y);
            }
        }

        [Test]
        public void SetValueTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                var number1 = this.AdjustNumberIfOnlyValue(slider, 6);
                slider.Value = number1;
                Assert.AreEqual(number1, slider.Value);

                var number2 = this.AdjustNumberIfOnlyValue(slider, 4);
                slider.Value = number2;
                Assert.AreEqual(number2, slider.Value);
            }
        }

        [Test]
        public void SmallIncrementTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                this.ResetToCenter(slider);
                slider.SmallIncrement();
                Assert.AreEqual(this.AdjustNumberIfOnlyValue(slider, 6), slider.Value);
            }
        }

        [Test]
        public void SmallDecrementTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                this.ResetToCenter(slider);
                slider.SmallDecrement();
                Assert.AreEqual(this.AdjustNumberIfOnlyValue(slider, 4), slider.Value);
            }
        }

        [Test]
        public void LargeIncrementTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                this.ResetToCenter(slider);
                slider.LargeIncrement();
                Assert.AreEqual(this.AdjustNumberIfOnlyValue(slider, 9), slider.Value);
            }
        }

        [Test]
        public void LargeDecrementTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider("Slider");
                this.ResetToCenter(slider);
                slider.LargeDecrement();
                Assert.AreEqual(this.AdjustNumberIfOnlyValue(slider, 1), slider.Value);
            }
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
