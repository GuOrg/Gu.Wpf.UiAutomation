namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using NUnit.Framework;

    public class WindowTests
    {
        private const string ExeFileName = "WpfApplication.exe";

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
