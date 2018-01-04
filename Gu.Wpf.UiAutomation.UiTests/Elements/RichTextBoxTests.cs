namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class RichTextBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using (var app = Application.Launch(ExeFileName, "RichTextBoxWindow"))
            {
                var window = app.MainWindow;
                var richTextBox = window.FindRichTextBox();
                Assert.IsInstanceOf<RichTextBox>(UiElement.FromAutomationElement(richTextBox.AutomationElement));
            }
        }
    }
}