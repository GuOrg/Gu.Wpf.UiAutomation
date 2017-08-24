namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ButtonTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory, 
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId")]
        [TestCase("XName")]
        [TestCase("Content")]
        public void FindButton(string key)
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow();
                var button = window.FindButton(key);
                Assert.NotNull(button);
            }
        }

        [Test]
        public void Click()
        {
            using (var app = Application.Launch(ExeFileName, "ButtonWindow"))
            {
                var window = app.MainWindow();
                var button = window.FindButton("Test Button");
                var textBlock = window.FindLabel("CountTextBlock");
                Assert.AreEqual("0", textBlock.Text);

                button.Click();
                Assert.AreEqual("1", textBlock.Text);
            }
        }
    }
}