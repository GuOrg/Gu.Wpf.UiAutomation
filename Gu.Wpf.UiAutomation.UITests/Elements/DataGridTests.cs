namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class DataGridTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadonlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void ColumnCount(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(2, dataGrid.ColumnCount);
            }
        }

        [TestCase("DataGrid", 2)]
        [TestCase("DataGrid100", 2)]
        [TestCase("DataGridNoHeaders", 0)]
        [TestCase("ReadonlyDataGrid", 2)]
        [TestCase("ReadonlyColumnsDataGrid", 2)]
        public void ColumnHeadersCount(string name, int expected)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expected, dataGrid.ColumnHeaders.Count);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadonlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void RowCellsCount(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(2, dataGrid.Rows[0].Cells.Count);
                Assert.AreEqual(2, dataGrid.Rows[1].Cells.Count);
                Assert.AreEqual(2, dataGrid.Rows[2].Cells.Count);
                Assert.AreEqual(2, dataGrid.Rows[3].Cells.Count);
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGrid100", 101)]
        [TestCase("DataGridNoHeaders", 4)]
        [TestCase("ReadonlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowCount(string name, int expectedRows)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expectedRows, dataGrid.RowCount);
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGrid100", 101)]
        [TestCase("DataGridNoHeaders", 4)]
        [TestCase("ReadonlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowsCount(string name, int expectedRows)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expectedRows, dataGrid.Rows.Count);
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGrid100", 101)]
        [TestCase("DataGridNoHeaders", 4)]
        [TestCase("ReadonlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowHeadersCount(string name, int expectedRows)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expectedRows, dataGrid.RowHeaders.Count);
            }
        }

        [TestCase("DataGrid", false)]
        [TestCase("DataGrid100", false)]
        [TestCase("DataGridNoHeaders", false)]
        [TestCase("ReadonlyDataGrid", true)]
        [TestCase("ReadonlyColumnsDataGrid", true)]
        public void IsReadOnly(string name, bool expected)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expected, dataGrid.IsReadOnly);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("ReadonlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void ColumnHeaders(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                var columns = dataGrid.Header.Columns;
                Assert.AreEqual(2, columns.Count);
                Assert.AreEqual("Id", columns[0].Text);
                Assert.AreEqual("Name", columns[1].Text);

                Assert.AreEqual(2, dataGrid.ColumnCount);
                Assert.AreEqual(2, dataGrid.ColumnHeaders.Count);
                Assert.AreEqual("Id", dataGrid.ColumnHeaders[0].Text);
                Assert.AreEqual("Name", dataGrid.ColumnHeaders[1].Text);
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGrid100", 101)]
        [TestCase("ReadonlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowHeaders(string name, int expectedRows)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                var rowHeaders = dataGrid.RowHeaders;
                Assert.AreEqual(expectedRows, rowHeaders.Count);
                Assert.AreEqual("Row 0", rowHeaders[0].Text);
                Assert.AreEqual("Row 1", rowHeaders[1].Text);
                Assert.AreEqual("Row 2", rowHeaders[2].Text);
                if (expectedRows == 4)
                {
                    Assert.AreEqual("Row 3", rowHeaders[3].Text);
                }
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadonlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void RowsAndCells(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                var rows = dataGrid.Rows;
                Assert.AreEqual("1", rows[0].Cells[0].Value);
                Assert.AreEqual("Item 1", rows[0].Cells[1].Value);
                Assert.AreEqual("2", rows[1].Cells[0].Value);
                Assert.AreEqual("Item 2", rows[1].Cells[1].Value);
                Assert.AreEqual("3", rows[2].Cells[0].Value);
                Assert.AreEqual("Item 3", rows[2].Cells[1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadonlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void Indexer(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);

                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);

                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        public void IndexerSetCellValue(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);

                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[0, 0].Value = "11";
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[2, 1].Value = "Item 5";
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 5", dataGrid[2, 1].Value);

                dataGrid[0, 0].Value = "111";
                Assert.AreEqual("111", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 5", dataGrid[2, 1].Value);
            }
        }

        [TestCase("DataGrid100", 99)]
        [TestCase("DataGrid100", 100)]
        public void IndexerSetCellValueWhenOffScreen(string name, int row)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                dataGrid[row, 0].Value = "-1";
                dataGrid[row, 1].Value = "Item -1";
                Assert.AreEqual("-1", dataGrid[row, 0].Value);
                Assert.AreEqual("Item -1", dataGrid[row, 1].Value);
            }
        }

        [TestCase("DataGrid100")]
        public void IndexerGetCellValueWhenOffScreen(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual("100", dataGrid[99, 0].Value);
                Assert.AreEqual("Item 100", dataGrid[99, 1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void NewItemPlaceholder(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(false, dataGrid[0, 0].IsNewItemPlaceholder);
                Assert.AreEqual(true, dataGrid[dataGrid.RowCount - 1, 0].IsNewItemPlaceholder);
                Assert.AreEqual(string.Empty, dataGrid[dataGrid.RowCount - 1, 0].Value);
                Assert.AreEqual(true, dataGrid[dataGrid.RowCount - 1, 1].IsNewItemPlaceholder);
                Assert.AreEqual(string.Empty, dataGrid[dataGrid.RowCount - 1, 1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        public void Enter(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);

                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[0, 0].Enter("11");
                Assert.AreEqual("1", dataGrid[0, 0].Value);

                dataGrid[1, 0].Click();
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);
            }
        }

        [TestCase("DataGrid", 0, 1)]
        [TestCase("DataGrid", 1, 2)]
        [TestCase("DataGrid", 2, 0)]
        [TestCase("DataGrid100", 0, 99)]
        [TestCase("DataGrid100", 1, 2)]
        [TestCase("DataGrid100", 99, 0)]
        [TestCase("DataGridNoHeaders", 0, 1)]
        [TestCase("DataGridNoHeaders", 1, 0)]
        [TestCase("DataGridNoHeaders", 2, 0)]
        public void SelectByIndexTest(string name, int index1, int index2)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                var selectedRow = dataGrid.Select(index1);
                Assert.AreEqual($"{index1 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index1 + 1}", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.SelectedItem;
                Assert.AreEqual($"{index1 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index1 + 1}", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.Select(index2);
                Assert.AreEqual($"{index2 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index2 + 1}", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.SelectedItem;
                Assert.AreEqual($"{index2 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index2 + 1}", selectedRow.Cells[1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        public void SelectByTextTest(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow();
                var dataGrid = window.FindDataGrid(name);
                var selectedRow = dataGrid.Select(1, "Item 2");
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 2", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.SelectedItem;
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 2", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.Select(1, "Item 3");
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 3", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.SelectedItem;
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 3", selectedRow.Cells[1].Value);
            }
        }
    }
}