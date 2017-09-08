namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ExpanderTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        private static readonly string WindowName = "ExpanderWindow";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("AutomationId", "1")]
        [TestCase("XName", "2")]
        [TestCase("Header", "Header")]
        public void FindExpander(string key, string header)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander(key);
                Assert.AreEqual(header, expander.Text);
                Assert.NotNull(expander.FindTextBlock());
            }
        }

        [Test]
        public void IsExpanded()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander("AutomationId");
                expander.IsExpanded = true;
                Assert.AreEqual(true, expander.IsExpanded);

                expander.IsExpanded = false;
                Assert.AreEqual(false, expander.IsExpanded);

                expander.IsExpanded = true;
                Assert.AreEqual(true, expander.IsExpanded);
            }
        }

        [Test]
        public void ExpandCollapse()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var expander = window.FindExpander("AutomationId");
                expander.IsExpanded = true;
                Assert.AreEqual(true, expander.IsExpanded);

                expander.Collapse();
                Assert.AreEqual(false, expander.IsExpanded);

                expander.Expand();
                Assert.AreEqual(true, expander.IsExpanded);
            }
        }
    }
}