namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class AutomationElementTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Parent()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox();
                Assert.AreEqual("Window", checkBox.Parent.ClassName);
            }
        }

        [Test]
        public void Window()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox();
                Assert.AreEqual("Window", checkBox.Window.ClassName);
            }
        }

        [Test]
        public void IsKeyboardFocusable()
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox();
                Assert.AreEqual(true, textBox.IsKeyboardFocusable);
            }
        }

        [Test]
        public void HasKeyboardFocus()
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox();
                Assert.AreEqual(false, textBox.HasKeyboardFocus);

                textBox.Click();
                Assert.AreEqual(true, textBox.HasKeyboardFocus);

                Keyboard.ClearFocus();
            }
        }
    }
}