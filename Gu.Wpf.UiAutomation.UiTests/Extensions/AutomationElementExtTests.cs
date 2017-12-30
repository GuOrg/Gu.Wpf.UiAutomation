namespace Gu.Wpf.UiAutomation.UiTests.Extensions
{
    using System.Windows.Automation;
    using NUnit.Framework;

    public class AutomationElementExtTests
    {
        [OneTimeTearDown]
        public void OneTimeSetUp()
        {
            Application.KillLaunched("WpfApplication.exe");
        }

        [Test]
        public void Parent()
        {
            using (var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow"))
            {
                var window = app.MainWindow;
                var parent = window.AutomationElement.Parent();
                Assert.AreEqual("#32769", parent.ClassName());
                Assert.AreEqual(AutomationElement.RootElement, parent);

                var checkbox = window.AutomationElement.FindFirst(TreeScope.Descendants, Gu.Wpf.UiAutomation.Condition.CheckBox);
                Assert.AreEqual(window.AutomationElement, checkbox.Parent());
            }
        }
    }
}
