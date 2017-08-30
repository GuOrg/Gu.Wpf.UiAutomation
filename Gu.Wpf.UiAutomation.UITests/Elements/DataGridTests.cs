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
        public void ColumnHeaders()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                var columns = dataGrid.Header.Columns;
                Assert.AreEqual(2, columns.Count);
                Assert.AreEqual(2, dataGrid.ColumnCount);
                Assert.AreEqual("Id", columns[0].Text);
                Assert.AreEqual("Name", columns[1].Text);

                Assert.AreEqual("Id", dataGrid.ColumnHeaders[0].Text);
                Assert.AreEqual("Name", dataGrid.ColumnHeaders[1].Text);
            }
        }

        [Test]
        public void RowHeaders()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                Assert.AreEqual(4, dataGrid.Rows.Count);
                Assert.AreEqual(4, dataGrid.RowHeaders.Count);
                Assert.AreEqual(4, dataGrid.RowCount);

                Assert.AreEqual("Row 0", dataGrid.RowHeaders[0].Text);
                Assert.AreEqual("Row 1", dataGrid.RowHeaders[1].Text);
                Assert.AreEqual("Row 2", dataGrid.RowHeaders[2].Text);
                Assert.AreEqual("Row 3", dataGrid.RowHeaders[3].Text);
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
        public void Indexer()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                Assert.AreEqual(4, dataGrid.RowCount);
                var rows = dataGrid.Rows;
                Assert.AreEqual(4, rows.Count);
                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);

                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);

                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                Assert.AreEqual("Item: {NewItemPlaceholder}, Column Display Index: 0", dataGrid[3, 0].Value);
                Assert.AreEqual("Item: {NewItemPlaceholder}, Column Display Index: 1", dataGrid[3, 1].Value);
            }
        }

        [Test]
        public void Enter()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();

                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);
                Assert.AreEqual("Item: {NewItemPlaceholder}, Column Display Index: 0", dataGrid[3, 0].Value);
                Assert.AreEqual("Item: {NewItemPlaceholder}, Column Display Index: 1", dataGrid[3, 1].Value);

                dataGrid[0, 0].Enter("11");
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);
                Assert.AreEqual("Item: {NewItemPlaceholder}, Column Display Index: 0", dataGrid[3, 0].Value);
                Assert.AreEqual("Item: {NewItemPlaceholder}, Column Display Index: 1", dataGrid[3, 1].Value);
            }
        }

        [Test]
        public void SelectByIndexTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid();
                var selectedRow = dataGrid.Select(1);
                this.CheckRow(selectedRow, "2", "Item 2");

                selectedRow = dataGrid.SelectedItem;
                this.CheckRow(selectedRow, "2", "Item 2");

                selectedRow = dataGrid.Select(2);
                this.CheckRow(selectedRow, "3", "Item 3");

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
            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual(cell1Value, cells[0].Value);
            Assert.AreEqual(cell2Value, cells[1].Value);
        }
    }
}