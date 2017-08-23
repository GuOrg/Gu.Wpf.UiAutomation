namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class TableTests : UITestBase
    {
        private Grid table;

        public TableTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [OneTimeSetUp]
        public void SelectTab()
        {
            var mainWindow = this.App.GetMainWindow(this.Automation);
            var tab = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tab)).AsTab();
            tab.SelectTabItem(1);
            var table = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("listView1")).AsGrid();
            this.table = table;
        }

        [Test]
        public void HeadersTest()
        {
            var table = this.table;
            Assert.That(table.ColumnHeaders.Length, Is.EqualTo(2));
        }

        [Test]
        public void HeaderAndColumnsTest()
        {
            var table = this.table;
            var header = table.Header;
            var columns = header.Columns;
            Assert.That(header, Is.Not.Null);
            Assert.That(columns, Has.Length.EqualTo(2));
            Assert.That(columns[0].Text, Is.EqualTo("Key"));
            Assert.That(columns[1].Text, Is.EqualTo("Value"));
        }

        [Test]
        public void RowsAndCellsTest()
        {
            var table = this.table;
            var rows = table.Rows;
            Assert.That(rows, Has.Length.EqualTo(3));
            this.CheckRow(rows[0], "1", "10");
            this.CheckRow(rows[1], "2", "20");
            this.CheckRow(rows[2], "3", "30");
        }

        private void CheckRow(GridRow tableRow, string cell1Value, string cell2Value)
        {
            var cells = tableRow.Cells;
            Assert.That(cells, Has.Length.EqualTo(2));
            this.CheckCellValue(cells[0], cell1Value);
            this.CheckCellValue(cells[1], cell2Value);
        }

        private void CheckCellValue(AutomationElement cell, string cellValue)
        {
            var cellText = cell.AsLabel();
            Assert.That(cellText.Text, Is.EqualTo(cellValue));
        }
    }
}
