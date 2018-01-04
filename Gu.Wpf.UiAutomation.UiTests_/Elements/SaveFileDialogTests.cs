namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class SaveFileDialogTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FromAutomationElement()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show SaveFileDialog").Click();
                var dialog = window.FindSaveFileDialog();
                dialog.Close();
            }
        }

        [Test]
        public void FileTextBox()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show SaveFileDialog").Click();
                var dialog = window.FindSaveFileDialog();
                Assert.NotNull(dialog.FileTextBox);
                dialog.SetFileName("C:\\Temp\\Foo.txt");
                dialog.Close();
            }
        }

        [Test]
        public void SaveButton()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show SaveFileDialog").Click();
                var dialog = window.FindSaveFileDialog();
                Assert.NotNull(dialog.SaveButton);
                dialog.Close();
            }
        }
    }
}
