namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class SeparatorTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using var app = Application.Launch(ExeFileName, "SeparatorWindow");
            var window = app.MainWindow;
            var separator = window.FindSeparator();
            Assert.IsInstanceOf<Separator>(UiElement.FromAutomationElement(separator.AutomationElement));
        }
    }
}
