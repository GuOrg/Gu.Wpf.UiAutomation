namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class RadioButtonTests : UITestBase
    {
        public RadioButtonTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void SelectSingleRadioButtonTest()
        {
            this.RestartApp();
            var radioButton = this.App.GetMainWindow(this.Automation).FindFirstDescendant(cf => cf.ByAutomationId("RadioButton1")).AsRadioButton();
            Assert.That(radioButton.IsSelected, Is.False);
            radioButton.Select();
            Assert.That(radioButton.IsSelected, Is.True);
        }

        [Test]
        public void SelectRadioButtonGroupTest()
        {
            this.RestartApp();
            var radioButton1 = this.App.GetMainWindow(this.Automation).FindFirstDescendant(cf => cf.ByAutomationId("RadioButton1")).AsRadioButton();
            var radioButton2 = this.App.GetMainWindow(this.Automation).FindFirstDescendant(cf => cf.ByAutomationId("RadioButton2")).AsRadioButton();

            Assert.That(radioButton1.IsSelected && radioButton2.IsSelected, Is.False);

            radioButton1.Select();
            Assert.That(radioButton1.IsSelected, Is.True);
            Assert.That(radioButton2.IsSelected, Is.False);

            radioButton2.Select();
            Assert.That(radioButton1.IsSelected, Is.False);
            Assert.That(radioButton2.IsSelected, Is.True);
        }
    }
}
