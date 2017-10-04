namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using NUnit.Framework;

    public class ProgressBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void MinimumValueTest()
        {
            using (var app = Application.Launch(ExeFileName, "ProgressBarWindow"))
            {
                var window = app.MainWindow;
                var bar = window.FindProgressBar();
                Assert.AreEqual(0, bar.Minimum);
            }
        }

        [Test]
        public void MaximumValueTest()
        {
            using (var app = Application.Launch(ExeFileName, "ProgressBarWindow"))
            {
                var window = app.MainWindow;
                var bar = window.FindProgressBar();
                Assert.AreEqual(100, bar.Maximum);
            }
        }

        [Test]
        public void ValueTest()
        {
            using (var app = Application.Launch(ExeFileName, "ProgressBarWindow"))
            {
                var window = app.MainWindow;
                var bar = window.FindProgressBar();
                Assert.AreEqual(50, bar.Value);
            }
        }
    }
}
