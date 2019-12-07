namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ProgressBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.Launch(ExeFileName, "ProgressBarWindow");
            var window = app.MainWindow;
            var progressBar = window.FindProgressBar();
            Assert.IsInstanceOf<ProgressBar>(UiElement.FromAutomationElement(progressBar.AutomationElement));
        }

        [Test]
        public void MinMaxAndValue()
        {
            using var app = Application.Launch(ExeFileName, "ProgressBarWindow");
            var window = app.MainWindow;
            var progressBar = window.FindProgressBar();
            Assert.AreEqual(0, progressBar.Minimum);
            Assert.AreEqual(100, progressBar.Maximum);
            Assert.AreEqual(50, progressBar.Value);
        }
    }
}
