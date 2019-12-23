namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ToolTipTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using var app = Application.Launch(ExeFileName, "ToolTipWindow");
            var window = app.MainWindow;
            var button = window.FindButton("With ToolTip");
            button.Click();
            var toolTip = button.FindToolTip();
            Assert.AreEqual(false, toolTip.IsOffscreen);
            Assert.AreEqual("Tool tip text.", toolTip.Text);
            Assert.IsInstanceOf<ToolTip>(UiElement.FromAutomationElement(toolTip.AutomationElement));

            button.Click();
            Assert.AreEqual(true, toolTip.IsOffscreen);
        }
    }
}
