namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class StatusBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using (var app = Application.Launch(ExeFileName, "StatusBarWindow"))
            {
                var window = app.MainWindow;
                var statusBar = window.FindStatusBar();
                Assert.NotNull(statusBar);
            }
        }
    }
}