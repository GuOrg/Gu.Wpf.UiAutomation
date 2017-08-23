namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class ComboBoxTests : UITestBase
    {
        private Window mainWindow;

        public ComboBoxTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [OneTimeSetUp]
        public void TestOneTimeSetup()
        {
            this.mainWindow = this.App.GetMainWindow(this.Automation);
        }

        [Test]
        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void SelectedItemTest(string comboBoxId)
        {
            var combo = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(comboBoxId)).AsComboBox();
            combo.Items[1].Select();
            var selectedItem = combo.SelectedItem;
            Assert.That(selectedItem, Is.Not.Null);
            Assert.That(selectedItem.Text, Is.EqualTo("Item 2"));
        }

        [Test]
        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void SelectByIndexTest(string comboBoxId)
        {
            var combo = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(comboBoxId)).AsComboBox();
            combo.Select(1);
            var selectedItem = combo.SelectedItem;
            Assert.That(selectedItem, Is.Not.Null);
            Assert.That(selectedItem.Text, Is.EqualTo("Item 2"));
        }

        [Test]
        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void SelectByTextTest(string comboBoxId)
        {
            var combo = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(comboBoxId)).AsComboBox();
            combo.Select("Item 2");
            var selectedItem = combo.SelectedItem;
            Assert.That(selectedItem, Is.Not.Null);
            Assert.That(selectedItem.Text, Is.EqualTo("Item 2"));
        }

        [Test]
        [TestCase("EditableCombo")]
        [TestCase("NonEditableCombo")]
        public void ExpandCollapseTest(string comboBoxId)
        {
            var combo = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(comboBoxId)).AsComboBox();
            combo.Expand();
            Assert.That(combo.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Expanded));
            combo.Collapse();
            Assert.That(combo.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Collapsed));
        }

        [Test]
        public void EditableTextTest()
        {
            var combo = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("EditableCombo")).AsComboBox();
            combo.EditableText = "Item 3";
            Assert.That(combo.SelectedItem, Is.Not.Null);
            Assert.That(combo.SelectedItem.Text, Is.EqualTo("Item 3"));
        }

        [Test]
        public void AssertMessageBoxCanBeRetrievedInSelection()
        {
            var combo = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("NonEditableCombo")).AsComboBox();
            combo.Items[3].Click();
            var window = Retry.While(() => this.mainWindow.FindFirstDescendant(cf => cf.ByClassName("#32770"))?.AsWindow(), w => w == null, TimeSpan.FromMilliseconds(1000));
            Assert.That(window, Is.Not.Null, "Expected a window that was shown when combobox item was selected");
            window.FindFirstDescendant(cf => cf.ByAutomationId("Close")).AsButton().Invoke();
        }
    }
}
