namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class FrameTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using var app = Application.Launch(ExeFileName, "FrameWindow");
            var window = app.MainWindow;
            var frame = window.FindFrame();
            Assert.IsInstanceOf<Frame>(UiElement.FromAutomationElement(frame.AutomationElement));
        }
    }
}