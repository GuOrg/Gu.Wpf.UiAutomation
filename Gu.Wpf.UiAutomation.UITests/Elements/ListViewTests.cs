namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class ListViewTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void RowAndColumnCount()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                Assert.AreEqual(3, listView.RowCount);
                Assert.AreEqual(2, listView.ColumnCount);
            }
        }

        [Test]
        public void HeaderAndColumns()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                var columns = listView.ColumnHeaders;
                Assert.AreEqual(2, columns.Count);
                Assert.AreEqual("Key", columns[0].Text);
                Assert.AreEqual("Value", columns[1].Text);
            }
        }

        [Test]
        public void RowsAndCells()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                Assert.AreEqual(3, listView.RowCount);
                var rows = listView.Rows;
                Assert.AreEqual(3, rows.Count);
                Assert.AreEqual(2, rows[0].Cells.Count);
                Assert.AreEqual("1", rows[0].Cells[0].Value);
                Assert.AreEqual("10", rows[0].Cells[1].Value);
                Assert.AreEqual(2, rows[1].Cells.Count);
                Assert.AreEqual("2", rows[1].Cells[0].Value);
                Assert.AreEqual("20", rows[1].Cells[1].Value);
                Assert.AreEqual(2, rows[2].Cells.Count);
                Assert.AreEqual("3", rows[2].Cells[0].Value);
                Assert.AreEqual("30", rows[2].Cells[1].Value);
            }
        }

        [Test]
        public void SelectByIndex()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                var selectedRow = listView.Select(1);
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("20", selectedRow.Cells[1].Value);

                selectedRow = listView.SelectedItem.AsGridRow();
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("20", selectedRow.Cells[1].Value);

                selectedRow = listView.Select(2);
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("30", selectedRow.Cells[1].Value);

                selectedRow = listView.SelectedItem.AsGridRow();
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("30", selectedRow.Cells[1].Value);
            }
        }

        [Test]
        public void SelectByTextTest()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "ListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                var selectedRow = listView.Select(1, "20");
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("20", selectedRow.Cells[1].Value);

                selectedRow = listView.SelectedItem.AsGridRow();
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("2", selectedRow.Cells[0].Value);
                Assert.AreEqual("20", selectedRow.Cells[1].Value);

                selectedRow = listView.Select(1, "30");
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("30", selectedRow.Cells[1].Value);

                selectedRow = listView.SelectedItem.AsGridRow();
                Assert.AreEqual(2, selectedRow.Cells.Count);
                Assert.AreEqual("3", selectedRow.Cells[0].Value);
                Assert.AreEqual("30", selectedRow.Cells[1].Value);
            }
        }
    }
}