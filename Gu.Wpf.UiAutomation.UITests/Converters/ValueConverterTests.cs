namespace Gu.Wpf.UiAutomation.UITests.Converters
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    [TestFixture(AutomationType.UIA2, TestApplicationType.Wpf)]
    [TestFixture(AutomationType.UIA3, TestApplicationType.Wpf)]
    public class ValueConverterTests : UITestBase
    {
        public ValueConverterTests(AutomationType automationType, TestApplicationType appType)
            : base(automationType, appType)
        {
        }

        [Test]
        public void GetControlType()
        {
            var window = this.App.GetMainWindow(this.Automation);
            var checkBox = window.FindFirstDescendant(cf => cf.ByName("Test Checkbox"));
            Assert.That(ControlType.CheckBox, Is.EqualTo(checkBox.Properties.ControlType));
        }
    }
}
