namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class CheckBoxTests : UITestBase
    {
        public CheckBoxTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void ToggleTest()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow(this.Automation);
            var checkBox = window.FindFirstDescendant(cf => cf.ByName("Test Checkbox")).AsCheckBox();
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.Off));
            checkBox.Toggle();
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.On));
        }

        [Test]
        public void SetStateTest()
        {
            var window = this.App.GetMainWindow(this.Automation);
            var checkBox = window.FindFirstDescendant(cf => cf.ByText("Test Checkbox")).AsCheckBox();
            checkBox.State = ToggleState.On;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.On));
            checkBox.State = ToggleState.Off;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.Off));
            checkBox.State = ToggleState.On;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.On));
        }

        [Test]
        public void ThreeWayToggleTest()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow(this.Automation);
            var checkBox = window.FindFirstDescendant(cf => cf.ByText("3-Way Test Checkbox")).AsCheckBox();
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.Off));
            checkBox.Toggle();
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.On));
            checkBox.Toggle();
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.Indeterminate));
        }

        [Test]
        public void ThreeWaySetStateTest()
        {
            var window = this.App.GetMainWindow(this.Automation);
            var checkBox = window.FindFirstDescendant(cf => cf.ByText("3-Way Test Checkbox")).AsCheckBox();
            checkBox.State = ToggleState.On;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.On));
            checkBox.State = ToggleState.Off;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.Off));
            checkBox.State = ToggleState.Indeterminate;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.Indeterminate));
            checkBox.State = ToggleState.On;
            Assert.That(checkBox.State, Is.EqualTo(ToggleState.On));
        }
    }
}
