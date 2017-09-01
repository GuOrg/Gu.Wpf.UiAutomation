namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class LabelTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Content", "Content")]
        public void FindLabel(string key, string header)
        {
            using (var app = Application.Launch(ExeFileName, "LabelWindow"))
            {
                var window = app.MainWindow;
                var label = window.FindLabel(key);
                Assert.AreEqual(header, label.Text);
                Assert.NotNull(label.FindTextBlock());
            }
        }
    }
}
