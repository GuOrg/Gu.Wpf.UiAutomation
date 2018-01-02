namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Windows.Automation;
    using NUnit.Framework;
    using Condition = Gu.Wpf.UiAutomation.Condition;

    public class DataGridRowTests
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
            using (var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow"))
            {
                var window = app.MainWindow;
                var header = (DataGridRow)window.FindFirst(TreeScope.Descendants, Condition.DataGridRow);
                Assert.IsInstanceOf<DataGridRow>(UiElement.FromAutomationElement(header.AutomationElement));
            }
        }

        [Test]
        public void Properties()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow"))
            {
                var window = app.MainWindow;
                var row = (DataGridRow)window.FindFirst(TreeScope.Descendants, Condition.DataGridRow);
                Assert.AreEqual("Row 1", row.Header.Text);
                Assert.NotNull(row.Header.TopHeaderGripper);
                Assert.NotNull(row.Header.BottomHeaderGripper);
            }
        }
    }
}