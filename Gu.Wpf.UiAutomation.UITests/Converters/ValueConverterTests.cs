namespace Gu.Wpf.UiAutomation.UITests.Converters
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class ValueConverterTests : UITestBase
    {
        public ValueConverterTests()
            : base(TestApplicationType.Wpf)
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
