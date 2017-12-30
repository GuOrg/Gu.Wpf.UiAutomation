namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class RangeValuePatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Slider()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow;
                var slider = window.FindSlider();
                Assert.NotNull(slider);
                var rvPattern = slider.AutomationElement.RangeValuePattern();
                Assert.AreEqual(false, rvPattern.Current.IsReadOnly);
                Assert.AreEqual(5, rvPattern.Current.Value);
                Assert.AreEqual(4, rvPattern.Current.LargeChange);
                Assert.AreEqual(1, rvPattern.Current.SmallChange);
                Assert.AreEqual(0, rvPattern.Current.Minimum);
                Assert.AreEqual(10, rvPattern.Current.Maximum);

                rvPattern.SetValue(6);
                Assert.AreEqual(6, rvPattern.Current.Value);

                rvPattern.SetValue(3);
                Assert.AreEqual(3, rvPattern.Current.Value);
            }
        }
    }
}
