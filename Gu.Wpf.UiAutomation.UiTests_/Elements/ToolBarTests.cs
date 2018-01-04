namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ToolBarTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using (var app = Application.Launch(ExeFileName, "ToolBarWindow"))
            {
                var window = app.MainWindow;
                var toolBar = window.FindToolBar();
                Assert.IsInstanceOf<ToolBar>(UiElement.FromAutomationElement(toolBar.AutomationElement));
            }
        }
    }
}
