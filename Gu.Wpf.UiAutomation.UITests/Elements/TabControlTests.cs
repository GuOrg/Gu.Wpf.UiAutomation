namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TabItemTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase(0, "x:Name", "1")]
        [TestCase(1, "Header", "2")]
        [TestCase(2, "AutomationProperties.AutomationId", "3")]
        public void Content(int index, string header, string content)
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
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
    }

    public class TabControlTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Items()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                Assert.AreEqual(3, tab.Items.Count);
                Assert.AreEqual("x:Name", tab.Items[0].Text);
                Assert.AreEqual("Header", tab.Items[1].Text);
                Assert.AreEqual("AutomationProperties.AutomationId", tab.Items[2].Text);

                for (var i = 0; i < tab.Items.Count; i++)
                {
                    var tabItem = tab.Items[i];
                    tabItem.Click();
                    Assert.AreEqual($"{i + 1}", tabItem.Content.AsTextBlock().Text);
                    Assert.AreEqual($"{i + 1}", tab.Content.AsTextBlock().Text);
                }
            }
        }

        [Test]
        public void SelectedIndex()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);

                tab.SelectedIndex = 1;
                Wait.UntilInputIsProcessed();
                Assert.AreEqual(1, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[1], tab.SelectedItem);

                tab.SelectedIndex = 0;
                Wait.UntilInputIsProcessed();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);
            }
        }

        [Test]
        public void SelectIndex()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);

                tab.Select(1);
                Wait.UntilInputIsProcessed();
                Assert.AreEqual(1, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[1], tab.SelectedItem);

                tab.Select(0);
                Wait.UntilInputIsProcessed();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);
            }
        }

        [Test]
        public void SelectText()
        {
            using (var app = Application.Launch(ExeFileName, "TabControlWindow"))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);

                tab.Select("Header");
                Wait.UntilInputIsProcessed();
                Assert.AreEqual(1, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[1], tab.SelectedItem);

                tab.Select("x:Name");
                Wait.UntilInputIsProcessed();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);
            }
        }
    }
}
