namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    [TestFixture(AutomationType.UIA2, TestApplicationType.Wpf)]
    [TestFixture(AutomationType.UIA3, TestApplicationType.Wpf)]
    public class LabelTests : UITestBase
    {
        public LabelTests(AutomationType automationType, TestApplicationType appType)
            : base(automationType, appType)
        {
        }

        [Test]
        public void GetText()
        {
            var window = this.App.GetMainWindow(this.Automation);
            var label = window.FindFirstDescendant(cf => cf.ByText("Test Label")).AsLabel();
            Assert.That(label, Is.Not.Null);
            Assert.That(label.Text, Is.EqualTo("Test Label"));
        }
    }
}
