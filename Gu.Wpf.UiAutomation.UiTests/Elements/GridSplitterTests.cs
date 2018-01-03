namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class GridSplitterTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using (var app = Application.Launch(ExeFileName, "GridSplitterWindow"))
            {
                var window = app.MainWindow;
                var gridSplitter = window.FindGridSplitter();
                Assert.IsInstanceOf<GridSplitter>(UiElement.FromAutomationElement(gridSplitter.AutomationElement));
            }
        }
    }
}
