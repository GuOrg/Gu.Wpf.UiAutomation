namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class ButtonTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase(null)]
        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButton(string key)
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton(key);
                Assert.NotNull(button);
            }
        }

        [TestCase("AutomationId", "AutomationProperties.AutomationId")]
        [TestCase("XName", "x:Name")]
        [TestCase("Content", "Content")]
        public void Text(string key, string expected)
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton(key);
                Assert.AreEqual(expected, button.Text);
            }
        }

        [TestCase("AutomationId", "AutomationProperties.AutomationId")]
        [TestCase("XName", "x:Name")]
        [TestCase("Content", "Content")]
        public void Content(string key, string expected)
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton(key);
                Assert.AreEqual(expected, button.Content.AsTextBlock().Text);
            }
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButtonThrowsWhenNotFound(string key)
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var exception = Assert.Throws<InvalidOperationException>(() => window.FindButton(key));
                Assert.AreEqual($"Did not find a Button with name {key}.", exception.Message);
            }
        }

        [Test]
        public void Click()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("Test Button");
                var textBlock = window.FindTextBlock("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);
            }
        }
    }
}