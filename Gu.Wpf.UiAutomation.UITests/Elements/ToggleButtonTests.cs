namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ToggleButtonTests
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
        public void FindToggleButton(string key)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton(key);
                Assert.NotNull(toggleButton);
            }
        }

        [Test]
        public void IsChecked()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton("Test ToggleButton");
                toggleButton.IsChecked = true;
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.IsChecked = false;
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.IsChecked = true;
                Assert.AreEqual(true, toggleButton.IsChecked);

                var exception = Assert.Throws<UiAutomationException>(() => toggleButton.IsChecked = null);
                Assert.AreEqual(
                    $"Setting AutomationId:SimpleToggleButton, Name:Test ToggleButton, ControlType:{toggleButton.LocalizedControlType}, FrameworkId:WPF .IsChecked to null failed.",
                    exception.Message);
            }
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton("3-Way Test ToggleButton");
                toggleButton.IsChecked = true;
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.IsChecked = false;
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.IsChecked = null;
                Assert.AreEqual(null, toggleButton.IsChecked);

                toggleButton.IsChecked = true;
                Assert.AreEqual(true, toggleButton.IsChecked);
            }
        }

        [Test]
        public void Click()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton("Test ToggleButton");
                toggleButton.IsChecked = false;
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.Click();
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.Click();
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.Click();
                Assert.AreEqual(true, toggleButton.IsChecked);
            }
        }

        [Test]
        public void ThreeStateClick()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow;
                var toggleButton = window.FindToggleButton("3-Way Test ToggleButton");
                toggleButton.IsChecked = false;
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.Click();
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.Click();
                Assert.AreEqual(null, toggleButton.IsChecked);

                toggleButton.Click();
                Assert.AreEqual(false, toggleButton.IsChecked);
            }
        }
    }
}