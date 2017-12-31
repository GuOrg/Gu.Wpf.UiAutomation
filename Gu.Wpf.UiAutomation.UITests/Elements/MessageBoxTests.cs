namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class MessageBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void MessageBox()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show MessageBox").Click();
                var messageBox = window.FindMessageBox();
                Assert.AreEqual("Caption text", messageBox.Caption);
                Assert.AreEqual("Message text", messageBox.Message);
                Assert.AreEqual("Message text", messageBox.FindLabel().Text);
                messageBox.Close();
            }
        }
    }
}