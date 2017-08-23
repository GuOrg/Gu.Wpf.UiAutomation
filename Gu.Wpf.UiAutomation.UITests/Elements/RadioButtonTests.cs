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
                Assert.NotNull(radioButton);
            }
        }

        [Test]
        public void IsChecked()
        {
            using (var app = Application.Launch(ExeFileName, "RadioButtonWindow"))
            {
                var window = app.MainWindow();
                var RadioButton = window.FindRadioButton("Test RadioButton");
                RadioButton.IsChecked = true;
                Assert.AreEqual(true, RadioButton.IsChecked);

                RadioButton.IsChecked = false;
                Assert.AreEqual(false, RadioButton.IsChecked);

                RadioButton.IsChecked = true;
                Assert.AreEqual(true, RadioButton.IsChecked);
            }
        }

        [Test]
        public void Toggle()
        {
            using (var app = Application.Launch(ExeFileName, "RadioButtonWindow"))
            {
                var window = app.MainWindow();
                var RadioButton = window.FindRadioButton("Test RadioButton");
                Assert.AreEqual(false, RadioButton.IsChecked);

                RadioButton.Toggle();
                Assert.AreEqual(true, RadioButton.IsChecked);

                RadioButton.Toggle();
                Assert.AreEqual(false, RadioButton.IsChecked);

                RadioButton.Toggle();
                Assert.AreEqual(true, RadioButton.IsChecked);
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
                radioButton.Select();
                Assert.That(radioButton.IsChecked, Is.True);
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

                radioButton1.Select();
                Assert.That(radioButton1.IsChecked, Is.True);
                Assert.That(radioButton2.IsChecked, Is.False);

                radioButton2.Select();
                Assert.That(radioButton1.IsChecked, Is.False);
                Assert.That(radioButton2.IsChecked, Is.True);
            }
        }
    }
}
