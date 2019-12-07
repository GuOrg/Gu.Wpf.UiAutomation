namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class RadioButtonTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindRadioButton(string key)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton = window.FindRadioButton(key);
            Assert.AreEqual(true, radioButton.IsEnabled);
            Assert.IsInstanceOf<RadioButton>(UiElement.FromAutomationElement(radioButton.AutomationElement));
        }

        [Test]
        public void IsChecked()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton1 = window.FindRadioButton("Test RadioButton");
            var radioButton2 = window.FindRadioButton("AutomationId");
            radioButton1.IsChecked = true;
            Assert.AreEqual(true, radioButton1.IsChecked);
            Assert.AreEqual(false, radioButton2.IsChecked);

            radioButton2.IsChecked = true;
            Assert.AreEqual(false, radioButton1.IsChecked);
            Assert.AreEqual(true, radioButton2.IsChecked);

            radioButton1.IsChecked = true;
            Assert.AreEqual(true, radioButton1.IsChecked);
            Assert.AreEqual(false, radioButton2.IsChecked);
        }

        [TestCase("Test RadioButton", "Test RadioButton")]
        public void Text(string name, string expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            Assert.AreEqual(expected, window.FindRadioButton(name).Text);
        }

        [Test]
        public void Click()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton1 = window.FindRadioButton("Test RadioButton");
            var radioButton2 = window.FindRadioButton("AutomationId");

            radioButton1.Click();
            Assert.AreEqual(true, radioButton1.IsChecked);
            Assert.AreEqual(false, radioButton2.IsChecked);

            radioButton2.Click();
            Assert.AreEqual(false, radioButton1.IsChecked);
            Assert.AreEqual(true, radioButton2.IsChecked);

            radioButton1.Click();
            Assert.AreEqual(true, radioButton1.IsChecked);
            Assert.AreEqual(false, radioButton2.IsChecked);
        }

        [Test]
        public void SelectSingleRadioButtonTest()
        {
            using var app = Application.Launch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton = window.FindRadioButton("RadioButton1");
            Assert.AreEqual(false, radioButton.IsChecked);

            radioButton.IsChecked = true;
            Assert.AreEqual(true, radioButton.IsChecked);
        }

        [Test]
        public void SelectRadioButtonGroupTest()
        {
            using var app = Application.Launch(ExeFileName, "RadioButtonWindow");
            var window = app.MainWindow;
            var radioButton1 = window.FindRadioButton("RadioButton1");
            var radioButton2 = window.FindRadioButton("RadioButton2");

            Assert.AreEqual(false, radioButton1.IsChecked);
            Assert.AreEqual(false, radioButton2.IsChecked);

            radioButton1.IsChecked = true;
            Assert.AreEqual(true, radioButton1.IsChecked);
            Assert.AreEqual(false, radioButton2.IsChecked);

            radioButton2.IsChecked = true;
            Assert.AreEqual(false, radioButton1.IsChecked);
            Assert.AreEqual(true, radioButton2.IsChecked);
        }
    }
}
