namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using System.Threading;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
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
                var window = app.MainWindow;
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
                var window = app.MainWindow;
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
        public void Value()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow;
                var slider = window.FindSlider("Slider");
                slider.Value = 6;
                Assert.AreEqual(6, slider.Value);

                slider.Value = 4;
                Assert.AreEqual(4, slider.Value);
            }
        }

        [Test]
        public void SmallIncrement()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow;
                var slider = window.FindSlider("Slider");
                Assert.AreEqual(5, slider.Value);
                slider.SmallIncrement();
                Assert.AreEqual(6, slider.Value);
            }
        }

        [Test]
        public void SmallDecrement()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow;
                var slider = window.FindSlider("Slider");
                Assert.AreEqual(5, slider.Value);
                slider.SmallDecrement();
                Assert.AreEqual(4, slider.Value);
            }
        }

        [Test]
        public void LargeIncrement()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow;
                var slider = window.FindSlider("Slider");
                Assert.AreEqual(5, slider.Value);
                slider.LargeIncrement();
                Assert.AreEqual(9, slider.Value);
            }
        }

        [Test]
        public void LargeDecrement()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow;
                var slider = window.FindSlider("Slider");
                Assert.AreEqual(5, slider.Value);
                slider.LargeDecrement();
                Assert.AreEqual(1, slider.Value);

                slider.LargeDecrement();
                Assert.AreEqual(0, slider.Value);
            }
        }
    }
}
