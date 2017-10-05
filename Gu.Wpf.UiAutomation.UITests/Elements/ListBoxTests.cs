namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ListBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("ListBox")]
        [TestCase("AutomationId")]
        public void Items(string key)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                var itemsControl = window.FindListBox(key);
                Assert.AreEqual(2, itemsControl.Items.Count);
            }
        }

        [Test]
        public void SelectByIndex()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                var listBox = window.FindListBox("BoundListBox");
                Assert.AreEqual(2, listBox.Items.Count);
                Assert.IsInstanceOf<ListBoxItem>(listBox.Items[0]);
                Assert.IsInstanceOf<ListBoxItem>(listBox.Items[1]);
                Assert.IsNull(listBox.SelectedItem);

                var item = listBox.Select(0);
                Assert.AreEqual("Johan", item.FindTextBlock().Text);
                Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);

                item = listBox.Select(1);
                Assert.AreEqual("Erik", item.FindTextBlock().Text);
                Assert.AreEqual("Erik", listBox.SelectedItem.FindTextBlock().Text);
            }
        }

        [Test]
        public void SelectByTextTest()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow"))
            {
                var window = app.MainWindow;
                var listBox = window.FindListBox("BoundListBox");
                var item = listBox.Select("Johan");
                Assert.AreEqual("Johan", item.FindTextBlock().Text);
                Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);

                item = listBox.Select("Erik");
                Assert.AreEqual("Erik", item.FindTextBlock().Text);
                Assert.AreEqual("Erik", listBox.SelectedItem.FindTextBlock().Text);

                item = listBox.Select("Johan");
                Assert.AreEqual("Johan", item.FindTextBlock().Text);
                Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);
            }
        }
    }
}