namespace Gu.Wpf.UiAutomation.UITests
{
    using System.IO;
    using NUnit.Framework;

    public class AutomationBaseTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [Test]
        public void FromPoint()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton();
                var fromPoint = window.Automation.FromPoint(button.GetClickablePoint()).AsButton();
                Assert.AreEqual(button.Text, fromPoint.Text);
            }
        }

        [Test]
        public void FocusedElement()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox();
                textBox.Text = "focused";
                textBox.Focus();
                var fromPoint = window.Automation.FocusedElement().AsTextBox();
                Assert.AreEqual("focused", fromPoint.Text);
            }
        }
    }
}