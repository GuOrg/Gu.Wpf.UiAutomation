namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class ListViewTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void GridPatternTest()
        {
            using (var app = Application.Launch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow();
                var listView = window.FindListView();
                Assert.AreEqual(2, listView.ColumnCount);
                Assert.AreEqual(3, listView.RowCount);
            }
        }

        [Test]
        public void HeaderAndColumnsTest()
        {
            using (var app = Application.Launch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow();
                var listView = window.FindListView();
                var columns = listView.Header.Columns;
                Assert.AreEqual(2, columns.Count);
                Assert.AreEqual("Key", columns[0].Text);
                Assert.AreEqual("Value", columns[1].Text);
            }
        }

        [Test]
        public void RowsAndCellsTest()
        {
            using (var app = Application.Launch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow();
                var listView = window.FindListView();
                Assert.AreEqual(3, listView.RowCount);
                var rows = listView.Rows;
                Assert.AreEqual(3, rows.Count);
                this.CheckRow(rows[0], "1", "10");
                this.CheckRow(rows[1], "2", "20");
                this.CheckRow(rows[2], "3", "30");
            }
        }

        [Test]
        public void SelectByIndexTest()
        {
            using (var app = Application.Launch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow();
                var listView = window.FindListView();
                listView.Select(1);
                var selectedRow = listView.SelectedItem;
                this.CheckRow(selectedRow, "2", "20");
                listView.Select(2);
                selectedRow = listView.SelectedItem;
                this.CheckRow(selectedRow, "3", "30");
            }
        }

        [Test]
        public void SelectByTextTest()
        {
            using (var app = Application.Launch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow();
                var listView = window.FindListView();
                listView.Select(1, "20");
                var selectedRow = listView.SelectedItem;
                this.CheckRow(selectedRow, "2", "20");
                listView.Select(1, "30");
                selectedRow = listView.SelectedItem;
                this.CheckRow(selectedRow, "3", "30");
            }
        }

        private void CheckRow(GridRow listViewRow, string cell1Value, string cell2Value)
        {
            var cells = listViewRow.Cells;
            Assert.AreEqual(2, cells.Length);
            Assert.AreEqual(cell1Value, cells[0].Value);
            Assert.AreEqual(cell2Value, cells[1].Value);
        }
    }
}