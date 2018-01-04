namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    [TestFixture]
    public class GridItemPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void DataGrid()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var cell = window.FindDataGrid()[0, 0];
                Assert.NotNull(cell);
                var pattern = cell.AutomationElement.GridItemPattern();
                Assert.AreEqual(0, pattern.Current.Row);
                Assert.AreEqual(1, pattern.Current.RowSpan);

                Assert.AreEqual(0, pattern.Current.Column);
                Assert.AreEqual(1, pattern.Current.ColumnSpan);

                Assert.AreEqual("DataGrid", pattern.Current.ContainingGrid.ClassName());
            }
        }
    }
}
