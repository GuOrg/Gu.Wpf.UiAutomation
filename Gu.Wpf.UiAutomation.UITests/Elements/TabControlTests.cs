namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TabControlTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Items()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var tab = window.FindTabControl();
                Assert.AreEqual(2, tab.Items.Length);
            }
        }

        [Test]
        public void SelectedIndex()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var tab = window.FindTabControl();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);

                tab.SelectedIndex = 1;
                Helpers.WaitUntilInputIsProcessed();
                Assert.AreEqual(1, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[1], tab.SelectedItem);

                tab.SelectedIndex = 0;
                Helpers.WaitUntilInputIsProcessed();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);
            }
        }

        [Test]
        public void SelectIndex()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var tab = window.FindTabControl();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);

                tab.Select(1);
                Helpers.WaitUntilInputIsProcessed();
                Assert.AreEqual(1, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[1], tab.SelectedItem);

                tab.Select(0);
                Helpers.WaitUntilInputIsProcessed();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);
            }
        }

        [Test]
        public void SelectText()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var tab = window.FindTabControl();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);

                tab.Select("Complex Controls");
                Helpers.WaitUntilInputIsProcessed();
                Assert.AreEqual(1, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[1], tab.SelectedItem);

                tab.Select("Simple Controls");
                Helpers.WaitUntilInputIsProcessed();
                Assert.AreEqual(0, tab.SelectedIndex);
                Assert.AreEqual(tab.Items[0], tab.SelectedItem);
            }
        }
    }
}
