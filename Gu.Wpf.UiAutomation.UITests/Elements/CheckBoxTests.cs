namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class CheckBoxTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindCheckBox(string key)
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow();
                var checkBox = window.FindCheckBox(key);
                Assert.NotNull(checkBox);
            }
        }

        [Test]
        public void IsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow();
                var checkBox = window.FindCheckBox("Test Checkbox");
                checkBox.IsChecked = true;
                Assert.AreEqual(true, checkBox.IsChecked);

                checkBox.IsChecked = false;
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.IsChecked = true;
                Assert.AreEqual(true, checkBox.IsChecked);

                var exception = Assert.Throws<UiAutomationException>(() => checkBox.IsChecked = null);
                Assert.AreEqual(
                    "Setting AutomationId:SimpleCheckBox, Name:Test Checkbox, ControlType:check box, FrameworkId:WPF .IsChecked to null failed.",
                    exception.Message);
            }
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow();
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
        }

        [Test]
        public void Toggle()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow();
                var checkBox = window.FindCheckBox("Test Checkbox");
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.Toggle();
                Assert.AreEqual(true, checkBox.IsChecked);

                checkBox.Toggle();
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.Toggle();
                Assert.AreEqual(true, checkBox.IsChecked);
            }
        }

        [Test]
        public void ThreeStateToggle()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow();
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
}
