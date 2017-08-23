namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class RadioButtonTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindRadioButton(string key)
        {
            using (var app = Application.Launch(ExeFileName, "RadioButtonWindow"))
            {
                var window = app.MainWindow();
                var radioButton = window.FindRadioButton(key);
                Assert.AreEqual(true, radioButton.IsEnabled);
                Assert.NotNull(radioButton);
            }
        }

        [Test]
        public void IsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "RadioButtonWindow"))
            {
                var window = app.MainWindow();
                var radioButton = window.FindRadioButton("Test RadioButton");
                radioButton.IsChecked = true;
                Assert.AreEqual(true, radioButton.IsChecked);

                radioButton.IsChecked = false;
                Assert.AreEqual(false, radioButton.IsChecked);

                radioButton.IsChecked = true;
                Assert.AreEqual(true, radioButton.IsChecked);
            }
        }

        [Test]
        public void Toggle()
        {
            using (var app = Application.Launch(ExeFileName, "RadioButtonWindow"))
            {
                var window = app.MainWindow();
                var radioButton = window.FindRadioButton("Test RadioButton");
                Assert.AreEqual(false, radioButton.IsChecked);

                radioButton.Toggle();
                Assert.AreEqual(true, radioButton.IsChecked);

                radioButton.Toggle();
                Assert.AreEqual(false, radioButton.IsChecked);

                radioButton.Toggle();
                Assert.AreEqual(true, radioButton.IsChecked);
            }
        }

        [Test]
        public void SelectSingleRadioButtonTest()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var radioButton = window.FindFirstDescendant(cf => cf.ByAutomationId("RadioButton1")).AsRadioButton();
                Assert.AreEqual(false, radioButton.IsChecked);

                radioButton.IsChecked = true;
                Assert.AreEqual(true, radioButton.IsChecked);
            }
        }

        [Test]
        public void SelectRadioButtonGroupTest()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var radioButton1 = window.FindFirstDescendant(cf => cf.ByAutomationId("RadioButton1")).AsRadioButton();
                var radioButton2 = window.FindFirstDescendant(cf => cf.ByAutomationId("RadioButton2")).AsRadioButton();

                Assert.That(radioButton1.IsChecked && radioButton2.IsChecked, Is.False);

                radioButton1.IsChecked = true;
                Assert.AreEqual(true, radioButton1.IsChecked);
                Assert.AreEqual(false, radioButton2.IsChecked);

                radioButton2.IsChecked = true;
                Assert.AreEqual(false, radioButton1.IsChecked);
                Assert.AreEqual(true, radioButton2.IsChecked);
            }
        }
    }
}
