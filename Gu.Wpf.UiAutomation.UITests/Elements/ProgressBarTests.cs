namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class ProgressBarTests : UITestBase
    {
        public ProgressBarTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void MinimumValueTest()
        {
            var bar = this.GetProgressBar();
            Assert.That(bar.Minimum, Is.EqualTo(0));
        }

        [Test]
        public void MaximumValueTest()
        {
            var bar = this.GetProgressBar();
            Assert.That(bar.Maximum, Is.EqualTo(100));
        }

        [Test]
        public void ValueTest()
        {
            var bar = this.GetProgressBar();
            Assert.That(bar.Value, Is.EqualTo(50));
        }

        private ProgressBar GetProgressBar()
        {
            var mainWindow = this.App.MainWindow();
            var element = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ProgressBar")).AsProgressBar();
            return element;
        }
    }
}
