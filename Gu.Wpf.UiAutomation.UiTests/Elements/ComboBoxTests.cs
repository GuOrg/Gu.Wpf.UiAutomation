namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using NUnit.Framework;

    public class ComboBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("EditableComboBox")]
        public void Find(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(key);
            Assert.IsInstanceOf<ComboBox>(UiElement.FromAutomationElement(comboBox.AutomationElement));
        }

        [TestCase("EditableComboBox", true)]
        [TestCase("NonEditableComboBox", false)]
        [TestCase("ReadOnlyComboBox", false)]
        [TestCase("DisabledComboBox", false)]
        public void IsEditable(string comboBoxId, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            Assert.AreEqual(expected, comboBox.IsEditable);
        }

        [TestCase("EditableComboBox", false)]
        [TestCase("NonEditableComboBox", false)]
        [TestCase("ReadOnlyComboBox", false)]
        [TestCase("ReadOnlyEditableComboBox", true)]
        public void IsReadOnly(string comboBoxId, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            Assert.AreEqual(expected, comboBox.IsReadOnly);
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        [TestCase("ReadOnlyComboBox")]
        [TestCase("ReadOnlyEditableComboBox")]
        ////[TestCase("DisabledComboBox")]
        public void Items(string id)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(id);
            CollectionAssert.AllItemsAreInstancesOfType(comboBox.Items, typeof(ComboBoxItem));
            Assert.AreEqual(new[] { "Item 1", "Item 2", "Item 3" }, comboBox.Items.Select(x => x.Text));
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectedItemTest(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            comboBox.Items[1].Select();
            var selectedItem = comboBox.SelectedItem;
            Assert.AreEqual("Item 2", selectedItem.Text);
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectByIndex(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            comboBox.Select(1);
            var selectedItem = comboBox.SelectedItem;
            Assert.AreEqual("Item 2", selectedItem.Text);
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectByText(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);
            comboBox.Select("Item 2");
            var selectedItem = comboBox.SelectedItem;
            Assert.AreEqual("Item 2", selectedItem.Text);
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void ExpandCollapseTest(string comboBoxId)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox(comboBoxId);

            comboBox.Expand();
            Assert.AreEqual(true, comboBox.IsDropDownOpen);

            comboBox.Collapse();
            Assert.AreEqual(false, comboBox.IsDropDownOpen);
        }

        [Test]
        public void EditableTextTest()
        {
            using var app = Application.Launch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox("EditableComboBox");
            comboBox.EditableText = "Item 3";
            Assert.AreEqual("Item 3", comboBox.SelectedItem.Text);
        }

        [Test]
        public void Enter()
        {
            using var app = Application.Launch(ExeFileName, "ComboBoxWindow");
            var window = app.MainWindow;
            var comboBox = window.FindComboBox("EditableComboBox");
            comboBox.Enter("Item 3");
            Assert.AreEqual("Item 3", comboBox.SelectedItem.Text);
        }
    }
}
