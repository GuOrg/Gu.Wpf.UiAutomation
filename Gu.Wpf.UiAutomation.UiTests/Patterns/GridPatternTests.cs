namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class GridPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void DataGrid()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid();
            Assert.NotNull(dataGrid);
            var pattern = dataGrid.AutomationElement.GridPattern();
            Assert.AreEqual(2, pattern.Current.ColumnCount);
            Assert.AreEqual(4, pattern.Current.RowCount);

            var item = pattern.GetItem(1, 1);
            Assert.AreEqual("Item 2", item.Name());
        }
    }
}
