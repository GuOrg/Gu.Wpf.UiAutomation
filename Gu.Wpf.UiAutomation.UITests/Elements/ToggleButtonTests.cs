namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ToggleButtonTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindToggleButton(string key)
        {
            using (var app = Application.Launch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow();
                var toggleButton = window.FindToggleButton(key);
                Assert.NotNull(toggleButton);
            }
        }

        [Test]
        public void IsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow();
                var toggleButton = window.FindToggleButton("Test ToggleButton");
                toggleButton.IsChecked = true;
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.IsChecked = false;
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.IsChecked = true;
                Assert.AreEqual(true, toggleButton.IsChecked);

                var exception = Assert.Throws<UiAutomationException>(() => toggleButton.IsChecked = null);
                Assert.AreEqual(
                    "Setting AutomationId:SimpleToggleButton, Name:Test ToggleButton, ControlType:button, FrameworkId:WPF .IsChecked to null failed.",
                    exception.Message);
            }
        }

        [Test]
        public void ThreeStateIsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow();
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
        public void Toggle()
        {
            using (var app = Application.Launch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow();
                var toggleButton = window.FindToggleButton("Test ToggleButton");
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.Toggle();
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.Toggle();
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.Toggle();
                Assert.AreEqual(true, toggleButton.IsChecked);
            }
        }

        [Test]
        public void ThreeStateToggle()
        {
            using (var app = Application.Launch(ExeFileName, "ToggleButtonWindow"))
            {
                var window = app.MainWindow();
                var toggleButton = window.FindToggleButton("3-Way Test ToggleButton");
                Assert.AreEqual(false, toggleButton.IsChecked);

                toggleButton.Toggle();
                Assert.AreEqual(true, toggleButton.IsChecked);

                toggleButton.Toggle();
                Assert.AreEqual(null, toggleButton.IsChecked);

                toggleButton.Toggle();
                Assert.AreEqual(false, toggleButton.IsChecked);
            }
        }
    }
}