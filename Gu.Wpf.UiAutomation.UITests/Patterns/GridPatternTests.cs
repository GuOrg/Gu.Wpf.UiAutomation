namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class GridPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void GridTest()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid();
                Assert.NotNull(dataGrid);
                var gridPattern = dataGrid.AutomationElement.GridPattern();
                Assert.AreEqual(2, gridPattern.Current.ColumnCount);
                Assert.AreEqual(4, gridPattern.Current.RowCount);

                var item = gridPattern.GetItem(1, 1);
                Assert.AreEqual("Item 2", item.Name());
            }
        }
    }
}
