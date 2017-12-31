namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ProgressBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void MinMaxAndValue()
        {
            using (var app = Application.Launch(ExeFileName, "ProgressBarWindow"))
            {
                var window = app.MainWindow;
                var bar = window.FindProgressBar();
                Assert.AreEqual(0, bar.Minimum);
                Assert.AreEqual(100, bar.Maximum);
                Assert.AreEqual(50, bar.Value);
            }
        }
    }
}
