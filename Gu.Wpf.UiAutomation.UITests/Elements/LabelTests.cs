namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class LabelTests : UITestBase
    {
        public LabelTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void GetText()
        {
            var window = this.App.GetMainWindow();
            var label = window.FindFirstDescendant(cf => cf.ByText("Test Label")).AsLabel();
            Assert.That(label, Is.Not.Null);
            Assert.That(label.Text, Is.EqualTo("Test Label"));
        }
    }
}
