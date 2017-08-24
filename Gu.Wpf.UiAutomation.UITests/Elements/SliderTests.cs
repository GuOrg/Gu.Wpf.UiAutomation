namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using System.Windows;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
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
                TestUtilities.AssertPointsAreSame(
                    thumb.Properties.BoundingRectangle.Value.Center(),
                    new Point(oldPos.X + 50, oldPos.Y), 1);
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
                Assert.That(slider.Value, Is.EqualTo(number1));
                var number2 = this.AdjustNumberIfOnlyValue(slider, 4);
                slider.Value = number2;
                Assert.That(slider.Value, Is.EqualTo(number2));
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
                Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 6)));
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
                Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 4)));
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
                Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 9)));
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
                Assert.That(slider.Value, Is.EqualTo(this.AdjustNumberIfOnlyValue(slider, 1)));
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
