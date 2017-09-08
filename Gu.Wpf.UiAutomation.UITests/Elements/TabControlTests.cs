namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TabControlTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");
        private static readonly string WindowName = "TabControlWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Items()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                Assert.AreEqual(4, tab.Items.Count);
                Assert.AreEqual("x:Name", tab.Items[0].Text);
                Assert.AreEqual("Header", tab.Items[1].Text);
                Assert.AreEqual("AutomationProperties.AutomationId", tab.Items[2].Text);
                Assert.AreEqual("WithItemsControl", tab.Items[3].Text);

                for (var i = 0; i < tab.Items.Count; i++)
                {
                    var tabItem = tab.Items[i];
                    tabItem.Click();
                    if (tabItem.ContentCollection.Count == 1)
                    {
                        Assert.AreEqual($"{i + 1}", tabItem.Content.AsTextBlock().Text);
                        Assert.AreEqual($"{i + 1}", tab.Content.AsTextBlock().Text);
                    }
                }
            }
        }

        [Test]
        public void SelectedIndex()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                tab.SelectedIndex = 0;
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
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                tab.SelectedIndex = 0;
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
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                tab.SelectedIndex = 0;
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
