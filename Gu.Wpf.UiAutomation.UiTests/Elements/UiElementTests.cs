namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using System.Drawing;

    using NUnit.Framework;

    public partial class UiElementTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Parent()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            var parent = button.Parent;
            Assert.AreEqual(window.AutomationElement, parent.AutomationElement);
        }

        [Test]
        public void Window()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            var buttonWindow = button.Window;
            Assert.AreEqual(window.AutomationElement, buttonWindow.AutomationElement);
            Assert.AreEqual(true, buttonWindow.IsMainWindow);
        }

        [Test]
        public void IsKeyboardFocusable()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox();
            Assert.AreEqual(true, textBox.IsKeyboardFocusable);
        }

        [Test]
        public void HasKeyboardFocus()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "TextBoxWindow");
            var window = app.MainWindow;
            var textBox = window.FindTextBox();
            Assert.AreEqual(false, textBox.HasKeyboardFocus);

            textBox.Click();
            Assert.AreEqual(true, textBox.HasKeyboardFocus);

            Keyboard.ClearFocus();
        }

        [Test]
        public void Size()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            Assert.AreEqual(200, button.ActualWidth);
            Assert.AreEqual(100, button.ActualHeight);
        }

        [Test]
        public void RenderBounds()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            window.MoveTo(100, 200);
            if (WindowsVersion.IsWindows7())
            {
                Assert.AreEqual(new System.Windows.Rect(150, 311, 200, 100), button.Bounds);
                Assert.AreEqual(new System.Windows.Rect(100, 200, 300, 300), window.Bounds);
                Assert.AreEqual(new System.Windows.Rect(50, 111, 200, 100), button.RenderBounds);
            }
            else
            {
                Assert.AreEqual(new System.Windows.Rect(150, 311, 200, 100), button.Bounds);
                Assert.AreEqual(new System.Windows.Rect(100, 200, 300, 300), window.Bounds);
                Assert.AreEqual(new System.Windows.Rect(50, 111, 200, 100), button.RenderBounds);
            }
        }

        [Test]
        public void DrawHighlight()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            _ = button.DrawHighlight();
            var bounds = button.Bounds;
            bounds.Inflate(2, 2);
            using var actual = Capture.Rectangle(bounds);
            ImageAssert.AreEqual(Properties.Resources.HiglightedButton, actual);
        }

        [Test]
        public void DrawHighlightBlocking()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            _ = button.DrawHighlight(blocking: true, color: Color.Blue, duration: TimeSpan.FromMilliseconds(500));
        }
    }
}
