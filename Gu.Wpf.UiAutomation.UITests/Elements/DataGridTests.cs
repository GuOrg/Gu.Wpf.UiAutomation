namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Linq;
    using NUnit.Framework;

    public class DataGridTests
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
                var dataGrid = window.FindDataGrid();
                Assert.IsInstanceOf<DataGrid>(UiElement.FromAutomationElement(dataGrid.AutomationElement));
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGridEmpty")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void RowCellsCount(string name)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                for (var i = 0; i < dataGrid.Rows.Count; i++)
                {
                    var row = dataGrid.Rows[i];
                    Assert.AreEqual(2, row.Cells.Count);
                    Assert.AreEqual(2, dataGrid.Row(0).Cells.Count);
                }
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGridEmpty", 1)]
        [TestCase("DataGrid10", 11)]
        [TestCase("DataGridNoHeaders", 4)]
        [TestCase("ReadOnlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowCount(string name, int expectedRows)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expectedRows, dataGrid.RowCount);
            }
        }

        [TestCase("DataGrid", new[] { "1, Item 1", "2, Item 2", "3, Item 3", ", " })]
        [TestCase("DataGridEmpty", new[] { ", " })]
        [TestCase("DataGrid10", new[] { "1, Item 1", "2, Item 2", "3, Item 3", "4, Item 4", "5, Item 5", "6, Item 6", "7, Item 7", "8, Item 8", "9, Item 9", "10, Item 10", ", " })]
        [TestCase("DataGridNoHeaders", new[] { "1, Item 1", "2, Item 2", "3, Item 3", ", " })]
        [TestCase("ReadOnlyDataGrid", new[] { "1, Item 1", "2, Item 2", "3, Item 3" })]
        [TestCase("ReadonlyColumnsDataGrid", new[] { "1, Item 1", "2, Item 2", "3, Item 3", ", " })]
        public void Rows(string name, string[] expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                CollectionAssert.AreEqual(expected, dataGrid.Rows.Select(x => string.Join(", ", x.Cells.Select(c => c.Value))));
            }
        }

        [TestCase("DataGrid", new[] { "Row 1", "Row 2", "Row 3", "" })]
        [TestCase("DataGridEmpty", new[] { "" })]
        [TestCase("DataGrid10", new[] { "Row 1", "Row 2", "Row 3", "Row 4", "Row 5", "Row 6", "Row 7", "Row 8", "Row 9", "Row 10", "" })]
        [TestCase("DataGridNoHeaders", new string[0])]
        [TestCase("ReadOnlyDataGrid", new[] { "Row 1", "Row 2", "Row 3" })]
        [TestCase("ReadonlyColumnsDataGrid", new[] { "Row 1", "Row 2", "Row 3", "" })]
        public void RowsHeaders(string name, string[] expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                CollectionAssert.AreEqual(expected, dataGrid.Rows.Select(x => x.Header.Text));
            }
        }

        [TestCase("DataGrid", new[] { "Row 1", "Row 2", "Row 3", "" })]
        [TestCase("DataGridEmpty", new[] { "" })]
        [TestCase("DataGrid10", new[] { "Row 1", "Row 2", "Row 3", "Row 4", "Row 5", "Row 6", "Row 7", "Row 8", "Row 9", "Row 10", "" })]
        [TestCase("DataGridNoHeaders", new string[0])]
        [TestCase("ReadOnlyDataGrid", new[] { "Row 1", "Row 2", "Row 3" })]
        [TestCase("ReadonlyColumnsDataGrid", new[] { "Row 1", "Row 2", "Row 3", "" })]
        public void RowHeaders(string name, string[] expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                CollectionAssert.AreEqual(expected, dataGrid.RowHeaders.Select(x => x.Text));
            }
        }

        [TestCase("DataGrid", 0, "Row 1")]
        [TestCase("DataGrid", 1, "Row 2")]
        [TestCase("DataGrid10", 0, "Row 1")]
        [TestCase("DataGrid10", 9, "Row 10")]
        [TestCase("ReadOnlyDataGrid", 0, "Row 1")]
        [TestCase("ReadonlyColumnsDataGrid", 0, "Row 1")]
        public void RowHeader(string name, int index, string expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expected, dataGrid.RowHeader(index).Text);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void RowsAndCells(string name)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                var rows = dataGrid.Rows;
                Assert.AreEqual("1", rows[0].Cells[0].Value);
                Assert.AreEqual("Item 1", rows[0].Cells[1].Value);
                Assert.AreEqual("2", rows[1].Cells[0].Value);
                Assert.AreEqual("Item 2", rows[1].Cells[1].Value);
                Assert.AreEqual("3", rows[2].Cells[0].Value);
                Assert.AreEqual("Item 3", rows[2].Cells[1].Value);
                if (!dataGrid.IsReadOnly)
                {
                    Assert.AreEqual(string.Empty, rows[dataGrid.RowCount - 1].Cells[0].Value);
                    Assert.AreEqual(string.Empty, rows[dataGrid.RowCount - 1].Cells[1].Value);
                }
            }
        }

        [TestCase("DataGrid", 4)]
        [TestCase("DataGridEmpty", 1)]
        [TestCase("DataGrid10", 11)]
        [TestCase("DataGridNoHeaders", 0)]
        [TestCase("ReadOnlyDataGrid", 3)]
        [TestCase("ReadonlyColumnsDataGrid", 4)]
        public void RowHeadersCount(string name, int expectedRows)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expectedRows, dataGrid.RowHeaders.Count);
            }
        }

        [TestCase("DataGrid", false)]
        [TestCase("DataGridEmpty", false)]
        [TestCase("DataGrid10", false)]
        [TestCase("DataGridNoHeaders", false)]
        [TestCase("TemplateColumnDataGrid", false)]
        [TestCase("ReadOnlyDataGrid", true)]
        [TestCase("ReadonlyColumnsDataGrid", false)]
        public void IsReadOnly(string name, bool expected)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expected, dataGrid.IsReadOnly);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGridEmpty")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        public void ColumnCount(string name)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(2, dataGrid.ColumnCount);
            }
        }

        [TestCase("DataGrid", 2)]
        [TestCase("DataGridEmpty", 2)]
        [TestCase("DataGrid10", 2)]
        [TestCase("DataGridNoHeaders", 0)]
        [TestCase("ReadOnlyDataGrid", 2)]
        [TestCase("ReadonlyColumnsDataGrid", 2)]
        public void ColumnHeadersCount(string name, int expected)
        {
            // We want launch here to repro a bug.
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expected, dataGrid.ColumnHeaders.Count);
            }
        }

        [TestCase("DataGrid", 2)]
        [TestCase("DataGridEmpty", 2)]
        [TestCase("DataGrid10", 2)]
        [TestCase("DataGridNoHeaders", 0)]
        [TestCase("ReadOnlyDataGrid", 2)]
        [TestCase("ReadonlyColumnsDataGrid", 2)]
        public void ColumnHeaders(string name, int expectedCount)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expectedCount, dataGrid.ColumnHeaders.Count);
                if (expectedCount == 0)
                {
                    return;
                }

                Assert.AreEqual("IntValue", dataGrid.ColumnHeaders[0].Text);
                Assert.AreEqual("StringValue", dataGrid.ColumnHeaders[1].Text);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void RowIndexer(string name)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual("1", dataGrid.Row(0).Cells[0].Value);
                Assert.AreEqual("Item 1", dataGrid.Row(0).Cells[1].Value);
                Assert.AreEqual("2", dataGrid.Row(1).Cells[0].Value);
                Assert.AreEqual("Item 2", dataGrid.Row(1).Cells[1].Value);
                Assert.AreEqual("3", dataGrid.Row(2).Cells[0].Value);
                Assert.AreEqual("Item 3", dataGrid.Row(2).Cells[1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void CellIndexer(string name)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual("1", dataGrid[0, 0].Value);
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
        [TestCase("DataGrid10", 0, 9)]
        [TestCase("DataGrid10", 1, 2)]
        [TestCase("DataGrid10", 9, 0)]
        [TestCase("DataGridNoHeaders", 0, 1)]
        [TestCase("DataGridNoHeaders", 1, 0)]
        [TestCase("DataGridNoHeaders", 2, 0)]
        public void SelectRowByIndex(string name, int index1, int index2)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                var selectedRow = dataGrid.Select(index1);
                Assert.AreEqual($"{index1 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index1 + 1}", selectedRow.Cells[1].Value);

                selectedRow = (DataGridRow)dataGrid.SelectedItem;
                Assert.AreEqual($"{index1 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index1 + 1}", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.Select(index2);
                Assert.AreEqual($"{index2 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index2 + 1}", selectedRow.Cells[1].Value);

                selectedRow = (DataGridRow)dataGrid.SelectedItem;
                Assert.AreEqual($"{index2 + 1}", selectedRow.Cells[0].Value);
                Assert.AreEqual($"Item {index2 + 1}", selectedRow.Cells[1].Value);
            }
        }

        [TestCase("SelectCellDataGrid", 2, 0)]
        public void SelectCellByIndex(string name, int index1, int index2)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                var selectedCell = dataGrid.Select(index1, index2);
                Assert.AreEqual($"{index1 + 1}", selectedCell.Value);

                selectedCell = (DataGridCell)dataGrid.SelectedItem;
                Assert.AreEqual($"{index1 + 1}", selectedCell.Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        public void SelectByTextTest(string name)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                var selectedRow = dataGrid.Select(1, "Item 2");
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 2", selectedRow.Cells[1].Value);

                selectedRow = (DataGridRow)dataGrid.SelectedItem;
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 2", selectedRow.Cells[1].Value);

                selectedRow = dataGrid.Select(1, "Item 3");
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 3", selectedRow.Cells[1].Value);

                selectedRow = (DataGridRow)dataGrid.SelectedItem;
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("Item 3", selectedRow.Cells[1].Value);
            }
        }
    }
}
