namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class TabItemTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

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
                Assert.AreEqual(header, tabItem.Text);
                Assert.AreEqual(header, tabItem.Header.AsTextBlock().Text);
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
                Assert.AreEqual(header, tabItem.Text);
                Assert.AreEqual(header, tabItem.Header.AsTextBlock().Text);
                Assert.AreEqual(content, tabItem.Content.AsTextBlock().Text);
                Assert.AreEqual(content, tabItem.ContentCollection[0].AsTextBlock().Text);
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
                Assert.AreEqual("WithItemsControl", tabItem.Text);
                Assert.AreEqual("WithItemsControl", tabItem.Header.AsTextBlock().Text);
                Assert.Throws<InvalidOperationException>(() => _ = tabItem.Content);
                var content = tabItem.ContentCollection;
                Assert.AreEqual(2, content.Count);
                Assert.AreEqual("1", content[0].AsTextBlock().Text);
                Assert.AreEqual("2", content[1].AsTextBlock().Text);
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
                Assert.AreEqual("TabItem must have be selected to get Content", exception.Message);
            }
        }
    }
}