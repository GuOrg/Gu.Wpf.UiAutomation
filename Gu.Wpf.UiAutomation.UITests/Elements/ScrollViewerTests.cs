namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ScrollViewerTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using (var app = Application.Launch(ExeFileName, "ScrollBarWindow"))
            {
                var window = app.MainWindow;
                var scrollViewer = window.FindScrollViewer();
                Assert.AreEqual("HorizontalScrollBar", scrollViewer.HorizontalScrollBar.AutomationId);
                Assert.AreEqual("VerticalScrollBar", scrollViewer.VerticalScrollBar.AutomationId);
            }
        }
    }
}