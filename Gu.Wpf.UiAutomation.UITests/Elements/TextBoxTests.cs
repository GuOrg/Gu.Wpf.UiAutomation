namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TextBoxTests
    {
        private static readonly string ExeFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("AutomationId")]
        [TestCase("XName")]
        public void FindTextBox(string key)
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow();
                var textBox = window.FindTextBox(key);
                Assert.AreEqual(true, textBox.IsEnabled);
                Assert.NotNull(textBox);
            }
        }

        [Test]
        public void DirectSetTest()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var textBox = window.FindTextBox("TextBox");
                Assert.AreEqual(string.Empty, textBox.Text);

                textBox.Text = "Hello World";
                Assert.AreEqual("Hello World", textBox.Text);

                textBox.Text = string.Empty;
                Assert.AreEqual(string.Empty, textBox.Text);

                textBox.Text = null;
                Assert.AreEqual(string.Empty, textBox.Text);
            }
        }

        [Test]
        public void EnterTest()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var textBox = window.FindTextBox("TextBox");

                textBox.Enter("Hello World");
                Assert.AreEqual("Hello World", textBox.Text);
            }
        }
    }
}
