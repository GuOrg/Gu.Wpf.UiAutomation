namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using System.IO;
    using NUnit.Framework;

    public class RangeValuePatternTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void RangeValuePatternTest()
        {
            using (var app = Application.Launch(ExeFileName, "SliderWindow"))
            {
                var window = app.MainWindow();
                var slider = window.FindSlider();
                Assert.NotNull(slider);
                var rvPattern = slider.Patterns.RangeValue.Pattern;
                Assert.AreEqual(false, rvPattern.IsReadOnly.Value);
                Assert.AreEqual(5, rvPattern.Value.Value);
                Assert.AreEqual(4, rvPattern.LargeChange.Value);
                Assert.AreEqual(1, rvPattern.SmallChange.Value);
                Assert.AreEqual(0, rvPattern.Minimum.Value);
                Assert.AreEqual(10, rvPattern.Maximum.Value);

                rvPattern.SetValue(6);
                Assert.AreEqual(6, rvPattern.Value.Value);

                rvPattern.SetValue(3);
                Assert.AreEqual(3, rvPattern.Value.Value);
            }
        }
    }
}
