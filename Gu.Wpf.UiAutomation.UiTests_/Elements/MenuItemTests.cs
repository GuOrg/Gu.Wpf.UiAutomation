namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class MenuItemTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Find()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "MenuWindow"))
            {
                var window = app.MainWindow;
                var menuItem = window.FindMenu().Items[0];
                Assert.IsInstanceOf<MenuItem>(UiElement.FromAutomationElement(menuItem.AutomationElement));
            }
        }
    }
}