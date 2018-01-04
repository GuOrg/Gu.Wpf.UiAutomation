namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TextBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [TestCase("AutomationId")]
        [TestCase("XName")]
        public void FindTextBox(string key)
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox(key);
                Assert.AreEqual(true, textBox.IsEnabled);
                Assert.IsInstanceOf<TextBox>(UiElement.FromAutomationElement(textBox.AutomationElement));
            }
        }

        [TestCase("AutomationId", false)]
        [TestCase("XName", false)]
        [TestCase("ReadOnlyTextBox", true)]
        public void IsReadOnly(string key, bool expected)
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox(key);
                Assert.AreEqual(expected, textBox.IsReadOnly);
            }
        }

        [Test]
        public void DirectSetText()
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox("TestTextBox");
                Assert.AreEqual("Test TextBox", textBox.Text);

                textBox.Text = "Hello World";
                Assert.AreEqual("Hello World", textBox.Text);

                textBox.Text = string.Empty;
                Assert.AreEqual(string.Empty, textBox.Text);

                textBox.Text = null;
                Assert.AreEqual(string.Empty, textBox.Text);
            }
        }

        [TestCase("Hello World")]
        public void EnterTest(string text)
        {
            using (var app = Application.Launch(ExeFileName, "TextBoxWindow"))
            {
                var window = app.MainWindow;
                var textBox = window.FindTextBox("TestTextBox");
                textBox.Enter(text);
                Assert.AreEqual(text, textBox.Text);
            }
        }
    }
}
