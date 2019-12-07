namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using System.Windows.Automation;
    using NUnit.Framework;

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
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var header = (DataGridRow)window.FindFirst(TreeScope.Descendants, Conditions.DataGridRow);
            Assert.IsInstanceOf<DataGridRow>(UiElement.FromAutomationElement(header.AutomationElement));
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var row = (DataGridRow)window.FindFirst(TreeScope.Descendants, Conditions.DataGridRow);
            Assert.AreEqual("Row 1", row.Header.Text);
            Assert.NotNull(row.Header.TopHeaderGripper);
            Assert.NotNull(row.Header.BottomHeaderGripper);
        }

        [Test]
        public void Cells()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SingleDataGridWindow");
            var window = app.MainWindow;
            var row = (DataGridRow)window.FindFirst(TreeScope.Descendants, Conditions.DataGridRow);
            Assert.AreEqual(2, row.Cells.Count);
            CollectionAssert.AllItemsAreInstancesOfType(row.Cells, typeof(DataGridCell));
            CollectionAssert.AreEqual(new[] { "1", "Item 1" }, row.Cells.Select(x => x.Value));
        }
    }
}
