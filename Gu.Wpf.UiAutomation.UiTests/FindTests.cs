namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class FindTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void TryFindFirstElementThatAppearAfterFewSeconds()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe");

            // This menu item appear after 2 sec in the application
            var condition = new AndCondition(
                Conditions.MenuItem,
                Conditions.ByNameOrAutomationId("DelayedMenuItem"));
            UiElement menuItem = null;

            Assert.DoesNotThrow(() => menuItem = app.MainWindow.FindFirst(TreeScope.Descendants, condition, TimeSpan.FromSeconds(10)));
            Assert.NotNull(menuItem as MenuItem);
        }
    }
}
