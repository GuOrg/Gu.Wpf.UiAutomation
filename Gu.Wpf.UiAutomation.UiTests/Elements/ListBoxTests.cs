namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
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
        public void Find(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox(key);
            Assert.IsInstanceOf<ListBox>(UiElement.FromAutomationElement(listBox.AutomationElement));
        }

        [TestCase("BoundListBox", new[] { "Johan", "Erik" })]
        [TestCase("ListBox", new[] { "1", "2" })]
        [TestCase("ListBox10", new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", })]
        public void Items(string name, string[] expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox(name);
            CollectionAssert.AreEqual(expected, listBox.Items.Select(x => x.Text));
        }

        [Test]
        public void ItemsWhenVirtual()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("ListBox10");
            var expected = new[]
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
            };
            CollectionAssert.AreEqual(expected, listBox.Items.Select(x => x.Text));
        }

        [Test]
        public void SelectByIndex()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("BoundListBox");
            Assert.AreEqual(2, listBox.Items.Count);
            Assert.IsInstanceOf<ListBoxItem>(listBox.Items[0]);
            Assert.IsInstanceOf<ListBoxItem>(listBox.Items[1]);
            Assert.IsNull(listBox.SelectedItem);
            Assert.AreEqual(-1, listBox.SelectedIndex);

            var item = listBox.Select(0);
            Assert.AreEqual("Johan", item.FindTextBlock().Text);
            Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);
            Assert.AreEqual(0, listBox.SelectedIndex);

            item = listBox.Select(1);
            Assert.AreEqual("Erik", item.FindTextBlock().Text);
            Assert.AreEqual("Erik", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);
            Assert.AreEqual(1, listBox.SelectedIndex);
        }

        [Test]
        public void SelectByIndexOutside()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("ListBox10");
            Assert.IsNull(listBox.SelectedItem);

            var item = listBox.Select(9);
            Assert.AreEqual("10", item.FindTextBlock().Text);
            Assert.AreEqual("10", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);

            item = listBox.Select(0);
            Assert.AreEqual("1", item.FindTextBlock().Text);
            Assert.AreEqual("1", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);
        }

        [Test]
        public void SelectByText()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("BoundListBox");
            var item = listBox.Select("Johan");
            Assert.AreEqual("Johan", item.FindTextBlock().Text);
            Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);

            item = listBox.Select("Erik");
            Assert.AreEqual("Erik", item.FindTextBlock().Text);
            Assert.AreEqual("Erik", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);

            item = listBox.Select("Johan");
            Assert.AreEqual("Johan", item.FindTextBlock().Text);
            Assert.AreEqual("Johan", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);
        }

        [Test]
        public void SelectByTextOutside()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ListBoxWindow");
            var window = app.MainWindow;
            var listBox = window.FindListBox("ListBox10");
            var item = listBox.Select("10");
            Assert.AreEqual("10", item.FindTextBlock().Text);
            Assert.AreEqual("10", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);

            item = listBox.Select("1");
            Assert.AreEqual("1", item.FindTextBlock().Text);
            Assert.AreEqual("1", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);

            item = listBox.Select("5");
            Assert.AreEqual("5", item.FindTextBlock().Text);
            Assert.AreEqual("5", listBox.SelectedItem.FindTextBlock().Text);
            Assert.AreEqual(item, listBox.SelectedItem);
        }
    }
}
