namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ToolTipTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string Window = "ToolTipWindow";

        [Test]
        public void FindImplicit()
        {
            using var app = Application.Launch(ExeFileName, Window);
            var window = app.MainWindow;
            var button = window.FindButton("With ToolTip");
            Mouse.Position = button.Bounds.Center();
            var toolTip = button.FindToolTip();
            Assert.AreEqual(false, toolTip.IsOffscreen);
            Assert.AreEqual("Tool tip text.", toolTip.Text);
            Assert.IsInstanceOf<ToolTip>(UiElement.FromAutomationElement(toolTip.AutomationElement));

            window.FindButton("Lose focus").Click();
            Assert.AreEqual(true, toolTip.IsOffscreen);
        }

        [Test]
        public void FindExplicit()
        {
            using var app = Application.Launch(ExeFileName, Window);
            var window = app.MainWindow;
            var button = window.FindButton("With explicit ToolTip");
            Mouse.Position = button.Bounds.Center();
            var toolTip = button.FindToolTip();
            Assert.AreEqual(false, toolTip.IsOffscreen);
            Assert.AreEqual("Explicit tool tip text.", toolTip.Text);
            Assert.IsInstanceOf<ToolTip>(UiElement.FromAutomationElement(toolTip.AutomationElement));

            window.FindButton("Lose focus").Click();
            Assert.AreEqual(true, toolTip.IsOffscreen);
        }
    }
}
