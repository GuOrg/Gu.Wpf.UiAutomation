namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class GroupBoxTests
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
        [TestCase("Header", "Header")]
        public void FindGroupBox(string key, string header)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "GroupBoxWindow"))
            {
                var window = app.MainWindow;
                var groupBox = window.FindGroupBox(key);
                Assert.AreEqual(header, groupBox.Text);
                Assert.NotNull(groupBox.FindTextBlock());
            }
        }
    }
}