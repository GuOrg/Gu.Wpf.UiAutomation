namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class CheckBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
            Retry.ResetTime();
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindCheckBox(string key)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox(key);
                Assert.NotNull(checkBox);
            }
        }

        [TestCase(null)]
        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindCheckBoxThrowsWhenNotFound(string key)
        {
            Retry.Time = TimeSpan.FromMilliseconds(10);
            using (var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                var exception = Assert.Throws<InvalidOperationException>(() => window.FindCheckBox(key));
                var expected = key == null
                    ? $"Did not find a CheckBox matching ControlType: CheckBox."
                    : $"Did not find a CheckBox matching (ControlType: CheckBox AND (Name: {key} OR AutomationId: {key})).";
                Assert.AreEqual(expected, exception.Message);
            }
        }

        [Test]
        public void IsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("Test Checkbox");
                checkBox.IsChecked = true;
                Assert.AreEqual(true, checkBox.IsChecked);

                checkBox.IsChecked = false;
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.IsChecked = true;
                Assert.AreEqual(true, checkBox.IsChecked);

                var exception = Assert.Throws<UiAutomationException>(() => checkBox.IsChecked = null);
                Assert.AreEqual(
                    $"Setting AutomationId:SimpleCheckBox, Name:Test Checkbox, ControlType:{checkBox.Properties.LocalizedControlType}, FrameworkId:WPF .IsChecked to null failed.",
                    exception.Message);
            }
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
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
        public void Click()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("Test Checkbox");
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.Click();
                Assert.AreEqual(true, checkBox.IsChecked);

                checkBox.Click();
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.Click();
                Assert.AreEqual(true, checkBox.IsChecked);
            }
        }

        [Test]
        public void ThreeStateClick()
        {
            using (var app = Application.Launch(ExeFileName, "CheckBoxWindow"))
            {
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("3-Way Test Checkbox");
                Assert.AreEqual(false, checkBox.IsChecked);

                checkBox.Click();
                Assert.AreEqual(true, checkBox.IsChecked);

                checkBox.Click();
                Assert.AreEqual(null, checkBox.IsChecked);

                checkBox.Click();
                Assert.AreEqual(false, checkBox.IsChecked);
            }
        }
    }
}
