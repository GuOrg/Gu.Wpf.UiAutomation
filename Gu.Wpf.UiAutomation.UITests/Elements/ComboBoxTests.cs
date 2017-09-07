namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ComboBoxTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("EditableComboBox", true)]
        [TestCase("NonEditableComboBox", false)]
        [TestCase("ReadOnlyComboBox", false)]
        public void IsEditable(string comboBoxId, bool expected)
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox(comboBoxId);
                Assert.AreEqual(expected, combo.IsEditable);
            }
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectedItemTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox(comboBoxId);
                combo.Items[1].Select();
                var selectedItem = combo.SelectedItem;
                Assert.AreEqual("Item 2", selectedItem.Text);
            }
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectByIndexTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox(comboBoxId);
                combo.Select(1);
                var selectedItem = combo.SelectedItem;
                Assert.AreEqual("Item 2", selectedItem.Text);
            }
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void SelectByTextTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox(comboBoxId);
                combo.Select("Item 2");
                var selectedItem = combo.SelectedItem;
                Assert.AreEqual("Item 2", selectedItem.Text);
            }
        }

        [TestCase("EditableComboBox")]
        [TestCase("NonEditableComboBox")]
        public void ExpandCollapseTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox(comboBoxId);

                combo.Expand();
                Assert.AreEqual(true, combo.IsDropDownOpen);

                combo.Collapse();
                Assert.AreEqual(false, combo.IsDropDownOpen);
            }
        }

        [Test]
        public void EditableTextTest()
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox("EditableComboBox");
                combo.EditableText = "Item 3";
                Assert.AreEqual("Item 3", combo.SelectedItem.Text);
            }
        }

        [Test]
        public void Enter()
        {
            using (var app = Application.Launch(ExeFileName, "ComboBoxWindow"))
            {
                var window = app.MainWindow;
                var combo = window.FindComboBox("EditableComboBox");
                combo.Enter("Item 3");
                Assert.AreEqual("Item 3", combo.SelectedItem.Text);
            }
        }
    }
}
