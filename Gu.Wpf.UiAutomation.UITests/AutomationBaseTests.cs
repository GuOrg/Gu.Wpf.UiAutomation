namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using NUnit.Framework;

    public class AutomationBaseTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [Test]
        public void FromPoint()
        {
            throw new NotImplementedException();
            //using (var app = Application.AttachOrLaunch(ExeFileName, "ButtonWindow"))
            //{
            //    var window = app.MainWindow;
            //    var button = window.FindButton();
            //    var fromPoint = window.Automation.FromPoint(button.GetClickablePoint()).AsButton();
            //    Assert.AreEqual(button.Text, fromPoint.Text);
            //}
        }

        [Test]
        public void FocusedElement()
        {
            throw new NotImplementedException();
            //using (var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow"))
            //{
            //    var window = app.MainWindow;
            //    var textBox = window.FindTextBox();
            //    textBox.Text = "focused";
            //    textBox.Focus();
            //    var fromPoint = window.FocusedElement().AsTextBox();
            //    Assert.AreEqual("focused", fromPoint.Text);
            //}
        }
    }
}