namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class WindowTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Close()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                window.Close();
            }
        }

        [Test]
        public void Dialog()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show Dialog").Click();
                var dialog = window.ModalWindows.Single();
                Assert.AreEqual("Message", dialog.FindTextBlock().Text);
                dialog.Close();
            }
        }
    }
}
