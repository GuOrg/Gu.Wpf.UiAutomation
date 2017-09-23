namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using NUnit.Framework;

    public class MessageBoxTests
    {
        private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

        [Test]
        public void MessageBox()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show MessageBox").Click();
                var messageBox = window.FindMessageBox();
                Assert.AreEqual("Message", messageBox.FindLabel().Text);
                messageBox.Close();
            }
        }
    }
}