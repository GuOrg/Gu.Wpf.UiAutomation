namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ListBoxTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("ListBox")]
        [TestCase("AutomationId")]
        public void Items(string key)
        {
            using (var app = Application.Launch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                var itemsControl = window.FindListBox(key);
                Assert.AreEqual(2, itemsControl.Items.Count);
            }
        }

        [Test]
        public void SelectByIndex()
        {
            using (var app = Application.Launch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                var listBox = window.FindListBox("BoundListBox");
                Assert.AreEqual(2, listBox.Items.Count);
                Assert.IsInstanceOf<ListBoxItem>(listBox.Items[0]);
                Assert.IsInstanceOf<ListBoxItem>(listBox.Items[1]);
                Assert.IsNull(listBox.SelectedItem);

                listBox.Select(0);
                Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);

                listBox.Select(1);
                Assert.AreEqual("Erik", listBox.SelectedItem.FindTextBlock().Text);
            }
        }
    }
}