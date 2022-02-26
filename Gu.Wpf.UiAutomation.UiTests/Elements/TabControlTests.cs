namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TabControlTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "TabControlWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tabControl = window.FindTabControl();
            Assert.IsInstanceOf<TabControl>(UiElement.FromAutomationElement(tabControl.AutomationElement));
        }

        [Test]
        public void Items()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            Assert.AreEqual(4, tab.Items.Count);
            Assert.AreEqual("x:Name", tab.Items[0].HeaderText);
            Assert.AreEqual("Header", tab.Items[1].HeaderText);
            Assert.AreEqual("AutomationProperties.AutomationId", tab.Items[2].HeaderText);
            Assert.AreEqual("WithItemsControl", tab.Items[3].HeaderText);

            for (var i = 0; i < tab.Items.Count; i++)
            {
                var tabItem = tab.Items[i];
                tabItem.Click();
                if (tabItem.ContentCollection.Count == 1)
                {
                    Assert.AreEqual($"{i + 1}", ((TextBlock)tabItem.Content).Text);
                    Assert.AreEqual($"{i + 1}", ((TextBlock)tab.Content).Text);
                }
            }
        }

        [Test]
        public void SelectedIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
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

        [Test]
        public void SelectIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            tab.SelectedIndex = 0;
            Assert.AreEqual(0, tab.SelectedIndex);
            Assert.AreEqual(tab.Items[0], tab.SelectedItem);

            Assert.AreEqual(tab.Items[1], tab.Select(1));
            Wait.UntilInputIsProcessed();
            Assert.AreEqual(1, tab.SelectedIndex);
            Assert.AreEqual(tab.Items[1], tab.SelectedItem);

            Assert.AreEqual(tab.Items[0], tab.Select(0));
            Wait.UntilInputIsProcessed();
            Assert.AreEqual(0, tab.SelectedIndex);
            Assert.AreEqual(tab.Items[0], tab.SelectedItem);
        }

        [Test]
        public void SelectText()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, WindowName);
            var window = app.MainWindow;
            var tab = window.FindTabControl();
            tab.SelectedIndex = 0;
            Assert.AreEqual(0, tab.SelectedIndex);
            Assert.AreEqual(tab.Items[0], tab.SelectedItem);

            Assert.AreEqual(tab.Items[1], tab.Select("Header"));
            Wait.UntilInputIsProcessed();
            Assert.AreEqual(1, tab.SelectedIndex);
            Assert.AreEqual(tab.Items[1], tab.SelectedItem);

            Assert.AreEqual(tab.Items[0], tab.Select("x:Name"));
            Wait.UntilInputIsProcessed();
            Assert.AreEqual(0, tab.SelectedIndex);
            Assert.AreEqual(tab.Items[0], tab.SelectedItem);
        }
    }
}
