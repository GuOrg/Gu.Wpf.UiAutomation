namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Globalization;
    using NUnit.Framework;

    public class SliderTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Find()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            Assert.IsInstanceOf<Slider>(UiElement.FromAutomationElement(slider.AutomationElement));
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;

            Assert.AreEqual(0, slider.Minimum);
            Assert.AreEqual(10, slider.Maximum);
            Assert.AreEqual(5, slider.Value);
            Assert.AreEqual(1, slider.SmallChange);
            Assert.AreEqual(4, slider.LargeChange);
            Assert.AreEqual(false, slider.IsOnlyValue);
        }

        [Test]
        public void SlideHorizontally()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;

            var thumb = slider.Thumb;
            Assert.AreEqual("350.5,240", thumb.Bounds.Center().ToString(CultureInfo.InvariantCulture));
            thumb.SlideHorizontally(50);
            Assert.AreEqual("397.5,240", thumb.Bounds.Center().ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void Value()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 6;
            Assert.AreEqual(6, slider.Value);

            slider.Value = 4;
            Assert.AreEqual(4, slider.Value);
        }

        [Test]
        public void SmallIncrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.AreEqual(5, slider.Value);

            slider.SmallIncrement();
            Assert.AreEqual(6, slider.Value);

            slider.SmallIncrement();
            Assert.AreEqual(7, slider.Value);

            slider.SmallIncrement();
            Assert.AreEqual(8, slider.Value);
        }

        [Test]
        public void SmallDecrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.AreEqual(5, slider.Value);

            slider.SmallDecrement();
            Assert.AreEqual(4, slider.Value);

            slider.SmallDecrement();
            Assert.AreEqual(3, slider.Value);
        }

        [Test]
        public void LargeIncrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.AreEqual(5, slider.Value);

            slider.LargeIncrement();
            Assert.AreEqual(9, slider.Value);

            slider.LargeIncrement();
            Assert.AreEqual(10, slider.Value);
        }

        [Test]
        public void LargeDecrement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SliderWindow");
            var window = app.MainWindow;
            var slider = window.FindSlider("Slider");
            slider.Value = 5;
            Assert.AreEqual(5, slider.Value);

            slider.LargeDecrement();
            Assert.AreEqual(1, slider.Value);

            slider.LargeDecrement();
            Assert.AreEqual(0, slider.Value);
        }
    }
}
