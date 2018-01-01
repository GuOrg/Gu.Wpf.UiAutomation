namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ScrollViewerTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Find()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow"))
            {
                var window = app.MainWindow;
                var scrollViewer = window.FindScrollViewer();
                Assert.AreEqual("HorizontalScrollBar", scrollViewer.HorizontalScrollBar.AutomationId);
                Assert.AreEqual("VerticalScrollBar", scrollViewer.VerticalScrollBar.AutomationId);
                Assert.IsInstanceOf<ScrollViewer>(UiElement.FromAutomationElement(scrollViewer.AutomationElement));
            }
        }

        [Test]
        public void ScrollPattern()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ScrollBarWindow"))
            {
                var window = app.MainWindow;
                var scrollViewer = window.FindScrollViewer();
                var pattern = scrollViewer.ScrollPattern.Current;
                Assert.AreEqual(0, pattern.HorizontalScrollPercent);
                Assert.AreEqual(66.75, pattern.HorizontalViewSize);
                Assert.AreEqual(true, pattern.HorizontallyScrollable);
                Assert.AreEqual(0, pattern.VerticalScrollPercent);
                //// Using a tolerance as there is a difference on Win7 & Win10
                Assert.AreEqual(61.25, pattern.VerticalViewSize, 1);
                Assert.AreEqual(true, pattern.VerticallyScrollable);
            }
        }
    }
}
