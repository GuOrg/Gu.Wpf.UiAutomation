namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class GroupBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        private static readonly string WindowName = "GroupBoxWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("xName", "2")]
        [TestCase("Header", "Header")]
        public void FindGroupBox(string key, string header)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var groupBox = window.FindGroupBox(key);
                Assert.AreEqual(header, groupBox.Text);
                Assert.NotNull(groupBox.FindTextBlock());
            }
        }

        [TestCase("AutomationId", "1")]
        [TestCase("xName", "2")]
        [TestCase("Header", "Header")]
        public void Header(string key, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var groupBox = window.FindGroupBox(key);
                Assert.AreEqual(expected, groupBox.Text);
                var header = groupBox.Header;
                Assert.AreEqual(ControlType.Text, header.ControlType);
                Assert.AreEqual("TextBlock", header.ClassName);
                Assert.AreEqual(expected, header.AsTextBlock().Text);
            }
        }

        [TestCase("AutomationId", "1")]
        [TestCase("xName", "2")]
        [TestCase("Header", "3")]
        public void Content(string key, string content)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var groupBox = window.FindGroupBox(key);
                Assert.AreEqual(content, groupBox.Content.AsTextBlock().Text);
                Assert.AreEqual(content, groupBox.ContentCollection[0].AsTextBlock().Text);
            }
        }

        [Test]
        public void ContentCollection()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var groupBox = window.FindGroupBox("WithItemsControl");
                Assert.AreEqual("WithItemsControl", groupBox.Text);
                Assert.AreEqual("WithItemsControl", groupBox.Header.AsTextBlock().Text);
                Assert.Throws<InvalidOperationException>(() => _ = groupBox.Content);
                var content = groupBox.ContentCollection;
                Assert.AreEqual(2, content.Count);
                Assert.AreEqual("1", content[0].AsTextBlock().Text);
                Assert.AreEqual("2", content[1].AsTextBlock().Text);
            }
        }

        [Test]
        public void ContentElements()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var groupBox = window.FindGroupBox("WithItemsControl");
                Assert.AreEqual("WithItemsControl", groupBox.Text);
                Assert.AreEqual("WithItemsControl", groupBox.Header.AsTextBlock().Text);
                Assert.Throws<InvalidOperationException>(() => _ = groupBox.Content);
                var content = groupBox.ContentElements(x => new TextBlock(x));
                Assert.AreEqual(2, content.Count);
                Assert.AreEqual("1", content[0].AsTextBlock().Text);
                Assert.AreEqual("2", content[1].AsTextBlock().Text);
            }
        }
    }
}