namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class TextBoxTests : UITestBase
    {
        public TextBoxTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void DirectSetTest()
        {
            var window = this.App.MainWindow();
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("TextBox")).AsTextBox();
            var text = textBox.Text;
            Assert.That(text, Is.Empty);
            var textToSet = "Hello World";
            textBox.Text = textToSet;
            text = textBox.Text;
            Assert.That(text, Is.EqualTo(textToSet));
            textBox.Text = string.Empty;
        }

        [Test]
        public void EnterTest()
        {
            var window = this.App.MainWindow();
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("TextBox")).AsTextBox();
            var text = textBox.Text;
            Assert.That(text, Is.Empty);
            var textToSet = "Hello World";
            textBox.Enter(textToSet);
            text = textBox.Text;
            Assert.That(text, Is.EqualTo(textToSet));
            textBox.Text = string.Empty;
        }
    }
}
