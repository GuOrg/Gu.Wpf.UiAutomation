namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class ComboBoxTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void SelectedItemTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var combo = window.FindComboBox(comboBoxId);
                combo.Items[1].Select();
                var selectedItem = combo.SelectedItem;
                Assert.AreEqual("Item 2", selectedItem.Text);
            }
        }

        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void SelectByIndexTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var combo = window.FindComboBox(comboBoxId);
                combo.Select(1);
                var selectedItem = combo.SelectedItem;
                Assert.AreEqual("Item 2", selectedItem.Text);
            }
        }

        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void SelectByTextTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var combo = window.FindComboBox(comboBoxId);
                combo.Select("Item 2");
                var selectedItem = combo.SelectedItem;
                Assert.AreEqual("Item 2", selectedItem.Text);
            }
        }

        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void ExpandCollapseTest(string comboBoxId)
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
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
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var combo = window.FindComboBox("EditableCombo");
                combo.EditableText = "Item 3";
                Assert.AreEqual("Item 3", combo.SelectedItem.Text);
            }
        }

        [Test]
        public void AssertMessageBoxCanBeRetrievedInSelection()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var combo = window.FindComboBox("NonEditableCombo");
                combo.Items[3].Click();
                var dialog = Retry.While(
                    () => window.FindFirstDescendant(cf => cf.ByClassName("#32770"))?.AsWindow(),
                    w => w == null,
                    TimeSpan.FromMilliseconds(1000));
                Assert.NotNull(dialog, "Expected a window that was shown when combobox item was selected");
                dialog.FindFirstDescendant(cf => cf.ByAutomationId("Close")).AsButton().Invoke();
            }
        }
    }
}
