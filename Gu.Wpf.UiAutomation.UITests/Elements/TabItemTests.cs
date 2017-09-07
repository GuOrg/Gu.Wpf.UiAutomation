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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase(0, "x:Name", "1")]
        [TestCase(1, "Header", "2")]
        [TestCase(2, "AutomationProperties.AutomationId", "3")]
        public void Content(int index, string header, string content)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                var tabItem = tab.Items[index];
                tabItem.Click();
                Assert.AreEqual(header, tabItem.Text);
                Assert.AreEqual(header, tabItem.Header.AsTextBlock().Text);
                Assert.AreEqual(content, tabItem.Content.AsTextBlock().Text);
            }
        }

        [Test]
        public void Content()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "TabControlWindow"))
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