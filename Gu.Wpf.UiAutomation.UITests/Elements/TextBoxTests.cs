namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    [TestFixture(AutomationType.UIA2, TestApplicationType.Wpf)]
    [TestFixture(AutomationType.UIA3, TestApplicationType.Wpf)]
    public class TextBoxTests : UITestBase
    {
        public TextBoxTests(AutomationType automationType, TestApplicationType appType) : base(automationType, appType)
        {
        }

        [Test]
        public void DirectSetTest()
        {
            var window = App.GetMainWindow(Automation);
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("TextBox")).AsTextBox();
            var text = textBox.Text;
            Assert.That(text, Is.Empty);
            var textToSet = "Hello World";
            textBox.Text = textToSet;
            text = textBox.Text;
            Assert.That(text, Is.EqualTo(textToSet));
            textBox.Text = "";
        }

        [Test]
        public void EnterTest()
        {
            var window = App.GetMainWindow(Automation);
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("TextBox")).AsTextBox();
            var text = textBox.Text;
            Assert.That(text, Is.Empty);
            var textToSet = "Hello World";
            textBox.Enter(textToSet);
            text = textBox.Text;
            Assert.That(text, Is.EqualTo(textToSet));
            textBox.Text = "";
        }
    }
}
