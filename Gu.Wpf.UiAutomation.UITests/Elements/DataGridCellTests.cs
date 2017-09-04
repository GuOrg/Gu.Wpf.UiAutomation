namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class DataGridCellTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [TestCase("DataGrid", false)]
        [TestCase("DataGrid100", false)]
        [TestCase("DataGridNoHeaders", false)]
        [TestCase("ReadOnlyDataGrid", true)]
        [TestCase("ReadonlyColumnsDataGrid", true)]
        public void CellsIsReadOnly(string name, bool expected)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual(expected, dataGrid[0, 0].IsReadOnly);
                Assert.AreEqual(expected, dataGrid[0, 1].IsReadOnly);
                Assert.AreEqual(expected, dataGrid[1, 0].IsReadOnly);
                Assert.AreEqual(expected, dataGrid[1, 1].IsReadOnly);
                Assert.AreEqual(expected, dataGrid[2, 0].IsReadOnly);
                Assert.AreEqual(expected, dataGrid[2, 1].IsReadOnly);
                if (name != "ReadOnlyDataGrid")
                {
                    Assert.AreEqual(expected, dataGrid[3, 0].IsReadOnly);
                    Assert.AreEqual(expected, dataGrid[3, 1].IsReadOnly);
                }
            }
        }
    }
}