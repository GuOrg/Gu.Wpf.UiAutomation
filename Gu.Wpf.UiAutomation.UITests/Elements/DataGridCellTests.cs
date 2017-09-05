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
        public void IsReadOnly(string name, bool expected)
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

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        public void Enter(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);

                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[0, 0].Enter("11");
                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[1, 0].Click();
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("11", dataGrid[0, 0].FindTextBlock().Text);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[0, 0].Enter("111");
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[1, 0].Click();
                Assert.AreEqual("111", dataGrid[0, 0].Value);
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
        public void SetValue(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);

                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[0, 0].Value = "11";
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("11", dataGrid[0, 0].FindTextBlock().Text);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

                dataGrid[2, 1].Value = "Item 5";
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.AreEqual("11", dataGrid[0, 0].FindTextBlock().Text);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 5", dataGrid[2, 1].Value);
                Assert.AreEqual("Item 5", dataGrid[2, 1].FindTextBlock().Text);

                dataGrid[0, 0].Value = "111";
                Assert.AreEqual("111", dataGrid[0, 0].Value);
                Assert.AreEqual("111", dataGrid[0, 0].FindTextBlock().Text);
                Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
                Assert.AreEqual("2", dataGrid[1, 0].Value);
                Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
                Assert.AreEqual("3", dataGrid[2, 0].Value);
                Assert.AreEqual("Item 5", dataGrid[2, 1].Value);
            }
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid100")]
        [TestCase("DataGridNoHeaders")]
        public void SetValueWhenFocused(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);

                var cell = dataGrid[0, 0];
                Assert.AreEqual("1", cell.Value);

                cell.Click();
                cell.Value = "11";
                Assert.AreEqual("11", cell.Value);
            }
        }

        [Test]
        public void SetValueUpdatesBinding()
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid("DataGrid");
                var readOnly = window.FindDataGrid("ReadOnlyDataGrid");

                Assert.AreEqual("1", dataGrid[0, 0].Value);
                Assert.AreEqual("1", readOnly[0, 0].Value);

                dataGrid[0, 0].Value = "11";
                Assert.AreEqual("11", dataGrid[0, 0].Value);
                Assert.Inconclusive("Figure out the least ugly way here.");
                Assert.AreEqual("11", readOnly[0, 0].Value);
            }
        }

        [TestCase("DataGrid100", 99)]
        [TestCase("DataGrid100", 100)]
        public void SetValueWhenOffScreen(string name, int row)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                dataGrid[row, 0].Value = "-1";
                dataGrid[row, 1].Value = "Item -1";
                Assert.AreEqual("-1", dataGrid[row, 0].Value);
                Assert.AreEqual("Item -1", dataGrid[row, 1].Value);
            }
        }

        [TestCase("DataGrid100")]
        public void GetValueWhenOffScreen(string name)
        {
            using (var app = Application.Launch(ExeFileName, "DataGridWindow"))
            {
                var window = app.MainWindow;
                var dataGrid = window.FindDataGrid(name);
                Assert.AreEqual("100", dataGrid[99, 0].Value);
                Assert.AreEqual("Item 100", dataGrid[99, 1].Value);
            }
        }
    }
}