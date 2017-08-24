namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class DataGridTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void GridPatternTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                Assert.AreEqual(2, dataGrid.ColumnCount);
                Assert.AreEqual(4, dataGrid.RowCount);
            }
        }

        [Test]
        public void HeaderAndColumnsTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                var columns = dataGrid.Header.Columns;
                Assert.AreEqual(2, columns.Count);
                Assert.AreEqual("Id", columns[0].Text);
                Assert.AreEqual("Name", columns[1].Text);
            }
        }

        [Test]
        public void RowsAndCellsTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                Assert.AreEqual(4, dataGrid.RowCount);
                var rows = dataGrid.Rows;
                Assert.AreEqual(4, rows.Count);
                this.CheckRow(rows[0], "1", "Item 1");
                this.CheckRow(rows[1], "2", "Item 2");
                this.CheckRow(rows[2], "3", "Item 3");
                this.CheckRow(rows[3], "Item: {NewItemPlaceholder}, Column Display Index: 0", "Item: {NewItemPlaceholder}, Column Display Index: 1");
            }
        }

        [Test]
        public void SelectByIndexTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                dataGrid.Select(1);
                var selectedRow = dataGrid.SelectedItem;
                this.CheckRow(selectedRow, "2", "Item 2");
                dataGrid.Select(2);
                selectedRow = dataGrid.SelectedItem;
                this.CheckRow(selectedRow, "3", "Item 3");
            }
        }

        [Test]
        public void SelectByTextTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                dataGrid.Select(1, "Item 2");
                var selectedRow = dataGrid.SelectedItem;
                this.CheckRow(selectedRow, "2", "Item 2");
                dataGrid.Select(1, "Item 3");
                selectedRow = dataGrid.SelectedItem;
                this.CheckRow(selectedRow, "3", "Item 3");
            }
        }

        private void CheckRow(GridRow dataGridRow, string cell1Value, string cell2Value)
        {
            var cells = dataGridRow.Cells;
            Assert.AreEqual(3, cells.Length);
            Assert.AreEqual(cell1Value, cells[0].Value);
            Assert.AreEqual(cell2Value, cells[1].Value);
        }
    }
}