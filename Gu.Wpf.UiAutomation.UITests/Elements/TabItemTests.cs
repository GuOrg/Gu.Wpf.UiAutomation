// ReSharper disable AssignmentIsFullyDiscarded
namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class TabItemTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private static readonly string WindowName = "TabControlWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase(0, "x:Name", "1")]
        [TestCase(1, "Header", "2")]
        [TestCase(2, "AutomationProperties.AutomationId", "3")]
        public void Header(int index, string header, string content)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                var tabItem = tab.Select(index);
                Assert.AreEqual(header, tabItem.HeaderText);
                Assert.AreEqual(header, ((TextBlock)tabItem.Header).Text);
            }
        }

        [TestCase(0, "x:Name", "1")]
        [TestCase(1, "Header", "2")]
        [TestCase(2, "AutomationProperties.AutomationId", "3")]
        public void Content(int index, string header, string content)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                var tabItem = tab.Select(index);
                Assert.AreEqual(header, tabItem.HeaderText);
                Assert.AreEqual(header, ((TextBlock)tabItem.Header).Text);
                Assert.AreEqual(content, ((TextBlock)tabItem.Content).Text);
                Assert.AreEqual(content, ((TextBlock)tabItem.ContentCollection[0]).Text);
            }
        }

        [Test]
        public void ItemsControlContent()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                var tabItem = tab.Select("WithItemsControl");
                Assert.AreEqual("WithItemsControl", tabItem.HeaderText);
                Assert.AreEqual("WithItemsControl", ((TextBlock)tabItem.Header).Text);
                Assert.Throws<InvalidOperationException>(() => _ = tabItem.Content);
                var content = tabItem.ContentCollection;
                Assert.AreEqual(2, content.Count);
                Assert.AreEqual("1", ((TextBlock)content[0]).Text);
                Assert.AreEqual("2", ((TextBlock)content[1]).Text);
            }
        }

        [Test]
        public void ContentThrowsWhenNotSelected()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                tab.SelectedIndex = 0;
                var exception = Assert.Throws<InvalidOperationException>(() => _ = tab.Items[1].Content);
                Assert.AreEqual("TabItem must have be selected to get Content.", exception.Message);
            }
        }
    }
}