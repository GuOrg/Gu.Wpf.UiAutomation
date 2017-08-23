namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class GridTests : UITestBase
    {
        private Grid grid;

        public GridTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [OneTimeSetUp]
        public void SelectTab()
        {
            var mainWindow = this.App.GetMainWindow(this.Automation);
            var tab = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tab)).AsTab();
            tab.SelectTabItem(1);
            var grid = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("listView1")).AsGrid();
            this.grid = grid;
        }

        [Test]
        public void GridPatternTest()
        {
            var grid = this.grid;
            Assert.That(grid.ColumnCount, Is.EqualTo(2));
            Assert.That(grid.RowCount, Is.EqualTo(3));
        }

        [Test]
        public void HeaderAndColumnsTest()
        {
            var grid = this.grid;
            var header = grid.Header;
            var columns = header.Columns;
            Assert.That(header, Is.Not.Null);
            Assert.That(columns, Has.Length.EqualTo(2));
            Assert.That(columns[0].Text, Is.EqualTo("Key"));
            Assert.That(columns[1].Text, Is.EqualTo("Value"));
        }

        [Test]
        public void RowsAndCellsTest()
        {
            var grid = this.grid;
            var rows = grid.Rows;
            Assert.That(rows, Has.Length.EqualTo(3));
            this.CheckRow(rows[0], "1", "10");
            this.CheckRow(rows[1], "2", "20");
            this.CheckRow(rows[2], "3", "30");
        }

        [Test]
        public void SelectByIndexTest()
        {
            var grid = this.grid;
            grid.Select(1);
            var selectedRow = grid.SelectedItem;
            this.CheckRow(selectedRow, "2", "20");
            grid.Select(2);
            selectedRow = grid.SelectedItem;
            this.CheckRow(selectedRow, "3", "30");
        }

        [Test]
        public void SelectByTextTest()
        {
            var grid = this.grid;
            grid.Select(1, "20");
            var selectedRow = grid.SelectedItem;
            this.CheckRow(selectedRow, "2", "20");
            grid.Select(1, "30");
            selectedRow = grid.SelectedItem;
            this.CheckRow(selectedRow, "3", "30");
        }

        private void CheckRow(GridRow gridRow, string cell1Value, string cell2Value)
        {
            var cells = gridRow.Cells;
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
