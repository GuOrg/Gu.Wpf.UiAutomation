namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ScrollBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FindHorizontalScrollBar()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollBar = window.FindHorizontalScrollBar();
            Assert.IsInstanceOf<HorizontalScrollBar>(scrollBar);
            Assert.AreEqual("HorizontalScrollBar", scrollBar.AutomationId);
            Assert.IsInstanceOf<HorizontalScrollBar>(UiElement.FromAutomationElement(scrollBar.AutomationElement));
        }

        [Test]
        public void FindVerticalScrollBar()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollBar = window.FindVerticalScrollBar();
            Assert.IsInstanceOf<VerticalScrollBar>(scrollBar);
            Assert.AreEqual("VerticalScrollBar", scrollBar.AutomationId);
            Assert.IsInstanceOf<VerticalScrollBar>(UiElement.FromAutomationElement(scrollBar.AutomationElement));
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow");
            var window = app.MainWindow;
            var scrollBar = window.FindVerticalScrollBar();
            Assert.AreEqual(0, scrollBar.Minimum);
            //// Using a tolerance as there is a difference on Win7 & Win10
            Assert.AreEqual(155, scrollBar.Maximum, 1);
            Assert.AreEqual(0, scrollBar.Value);
            Assert.AreEqual(0.1, scrollBar.SmallChange);
            Assert.AreEqual(1, scrollBar.LargeChange);
            Assert.AreEqual(false, scrollBar.IsReadOnly);
        }
    }
}
