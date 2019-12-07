// ReSharper disable AssignmentIsFullyDiscarded
namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class GroupBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        private const string WindowName = "GroupBoxWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void FindGroupBox(string key, string header)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox(key);
            Assert.AreEqual(header, groupBox.HeaderText);
            Assert.NotNull(groupBox.FindTextBlock());
            Assert.IsInstanceOf<GroupBox>(UiElement.FromAutomationElement(groupBox.AutomationElement));
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void Header(string key, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox(key);
            Assert.AreEqual(expected, groupBox.HeaderText);
            var header = groupBox.Header;
            Assert.AreEqual(expected, ((TextBlock)header).Text);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "3")]
        public void Content(string key, string content)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox(key);
            Assert.AreEqual(content, ((TextBlock)groupBox.Content).Text);
            Assert.AreEqual(content, ((TextBlock)groupBox.ContentCollection[0]).Text);
        }

        [Test]
        public void ContentCollection()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox("WithItemsControl");
            Assert.AreEqual("WithItemsControl", groupBox.HeaderText);
            Assert.AreEqual("WithItemsControl", ((TextBlock)groupBox.Header).Text);
            Assert.Throws<InvalidOperationException>(() => _ = groupBox.Content);
            var content = groupBox.ContentCollection;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("1", ((TextBlock)content[0]).Text);
            Assert.AreEqual("2", ((TextBlock)content[1]).Text);
        }

        [Test]
        public void ContentElements()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var groupBox = window.FindGroupBox("WithItemsControl");
            Assert.AreEqual("WithItemsControl", groupBox.HeaderText);
            Assert.AreEqual("WithItemsControl", ((TextBlock)groupBox.Header).Text);
            Assert.Throws<InvalidOperationException>(() => _ = groupBox.Content);
            var content = groupBox.ContentElements(x => new TextBlock(x));
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("1", content[0].Text);
            Assert.AreEqual("2", content[1].Text);
        }
    }
}
