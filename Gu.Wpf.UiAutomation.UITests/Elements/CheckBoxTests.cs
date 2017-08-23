namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class CheckBoxTests : UITestBase
    {
        public CheckBoxTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void IsChecked()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow();
            var checkBox = window.FindCheckBox("Test Checkbox");
            checkBox.IsChecked = true;
            Assert.AreEqual(true, checkBox.IsChecked);

            checkBox.IsChecked = false;
            Assert.AreEqual(false, checkBox.IsChecked);

            checkBox.IsChecked = true;
            Assert.AreEqual(true, checkBox.IsChecked);

            var exception = Assert.Throws<UiAutomationException>(() => checkBox.IsChecked = null);
            Assert.AreEqual("Setting AutomationId:SimpleCheckBox, Name:Test Checkbox, ControlType:check box, FrameworkId:WPF .IsChecked to null failed.", exception.Message);
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow();
            var checkBox = window.FindCheckBox("3-Way Test Checkbox");
            checkBox.IsChecked = true;
            Assert.AreEqual(true, checkBox.IsChecked);

            checkBox.IsChecked = false;
            Assert.AreEqual(false, checkBox.IsChecked);

            checkBox.IsChecked = null;
            Assert.AreEqual(null, checkBox.IsChecked);

            checkBox.IsChecked = true;
            Assert.AreEqual(true, checkBox.IsChecked);
        }

        [Test]
        public void Toggle()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow();
            var checkBox = window.FindCheckBox("Test Checkbox");
            Assert.AreEqual(false, checkBox.IsChecked);

            checkBox.Toggle();
            Assert.AreEqual(true, checkBox.IsChecked);

            checkBox.Toggle();
            Assert.AreEqual(false, checkBox.IsChecked);

            checkBox.Toggle();
            Assert.AreEqual(true, checkBox.IsChecked);
        }

        [Test]
        public void ThreeStateToggle()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow();
            var checkBox = window.FindCheckBox("3-Way Test Checkbox");
            Assert.AreEqual(false, checkBox.IsChecked);

            checkBox.Toggle();
            Assert.AreEqual(true, checkBox.IsChecked);

            checkBox.Toggle();
            Assert.AreEqual(null, checkBox.IsChecked);

            checkBox.Toggle();
            Assert.AreEqual(false, checkBox.IsChecked);
        }
    }
}
