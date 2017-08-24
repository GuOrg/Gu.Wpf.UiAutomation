namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TextBlockTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindTextBlock(string key)
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow();
                var textBlock = window.FindTextBlock(key);
                Assert.NotNull(textBlock?.Text);
            }
        }
    }
}