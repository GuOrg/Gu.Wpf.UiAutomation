namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TextBlockTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Content", "Content")]
        public void FindTextBlock(string key, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "TextBlockWindow"))
            {
                var window = app.MainWindow;
                var textBlock = window.FindTextBlock(key);
                Assert.AreEqual(expected, textBlock.Text);
            }
        }
    }
}