namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System;
    using NUnit.Framework;

    public class DataGridCellTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [TestCase("DataGrid", false)]
        [TestCase("DataGrid10", false)]
        [TestCase("DataGridNoHeaders", false)]
        [TestCase("ReadOnlyDataGrid", true)]
        [TestCase("ReadonlyColumnsDataGrid", true)]
        [TestCase("TemplateColumnDataGrid", false)]
        public void IsReadOnly(string name, bool expected)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
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

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("ReadOnlyDataGrid")]
        [TestCase("ReadonlyColumnsDataGrid")]
        [TestCase("TemplateColumnDataGrid")]
        public void ContainingGrid(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.AreEqual(dataGrid, dataGrid[0, 0].ContainingDataGrid);
            Assert.AreEqual(dataGrid, dataGrid[0, 1].ContainingDataGrid);
            Assert.AreEqual(dataGrid, dataGrid[1, 0].ContainingDataGrid);
            Assert.AreEqual(dataGrid, dataGrid[1, 1].ContainingDataGrid);
            Assert.AreEqual(dataGrid, dataGrid[2, 0].ContainingDataGrid);
            Assert.AreEqual(dataGrid, dataGrid[2, 1].ContainingDataGrid);
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void NewItemPlaceholder(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.AreEqual(false, dataGrid[0, 0].IsNewItemPlaceholder);
            Assert.AreEqual(true, dataGrid[dataGrid.RowCount - 1, 0].IsNewItemPlaceholder);
            Assert.AreEqual(string.Empty, dataGrid[dataGrid.RowCount - 1, 0].Value);
            Assert.AreEqual(true, dataGrid[dataGrid.RowCount - 1, 1].IsNewItemPlaceholder);
            Assert.AreEqual(string.Empty, dataGrid[dataGrid.RowCount - 1, 1].Value);
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        public void Enter(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
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
            Assert.AreEqual("11", dataGrid[0, 0].FindTextBox().Text);
            Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
            Assert.AreEqual("2", dataGrid[1, 0].Value);
            Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
            Assert.AreEqual("3", dataGrid[2, 0].Value);
            Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

            dataGrid[1, 1].Click();
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
        }

        [Test]
        public void EnterTemplateColumn()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid("TemplateColumnDataGrid");

            Assert.AreEqual("1", dataGrid[0, 0].Value);
            Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
            Assert.AreEqual("2", dataGrid[1, 0].Value);
            Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
            Assert.AreEqual("3", dataGrid[2, 0].Value);
            Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

            dataGrid[0, 0].Enter("11");
            Assert.AreEqual("11", dataGrid[0, 0].Value);
            Assert.AreEqual("11", dataGrid[0, 0].FindTextBox().Text);
            Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
            Assert.AreEqual("2", dataGrid[1, 0].Value);
            Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
            Assert.AreEqual("3", dataGrid[2, 0].Value);
            Assert.AreEqual("Item 3", dataGrid[2, 1].Value);

            dataGrid[0, 0].Enter("111");
            Assert.AreEqual("111", dataGrid[0, 0].Value);
            Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
            Assert.AreEqual("2", dataGrid[1, 0].Value);
            Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
            Assert.AreEqual("3", dataGrid[2, 0].Value);
            Assert.AreEqual("Item 3", dataGrid[2, 1].Value);
        }

        [TestCase("DataGrid")]
        [TestCase("SelectCellDataGrid")]
        public void EnterInvalidValue(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            Assert.AreEqual("1", cell.Value);

            cell.Enter("a");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.AreEqual("1", cell.Value);
            Assert.AreEqual("a", cell.FindTextBox().Text);

            cell.Enter("11");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.AreEqual("11", cell.Value);
            Assert.AreEqual("11", cell.FindTextBlock().Text);
        }

        [Test]
        public void EnterInvalidValueTemplateColumn()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid("TemplateColumnDataGrid");

            var cell = dataGrid[0, 0];
            Assert.AreEqual("1", cell.Value);

            cell.Enter("a");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.AreEqual("a", cell.Value);
            Assert.AreEqual("a", cell.FindTextBox().Text);

            cell.Enter("11");
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Keyboard.Type(Key.TAB);
            Assert.AreEqual("11", cell.Value);
            Assert.AreEqual("11", cell.FindTextBlock().Text);
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void SetValue(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
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

            dataGrid[0, 0].Value = "111";
            Assert.AreEqual("111", dataGrid[0, 0].Value);
            Assert.AreEqual("Item 1", dataGrid[0, 1].Value);
            Assert.AreEqual("2", dataGrid[1, 0].Value);
            Assert.AreEqual("Item 2", dataGrid[1, 1].Value);
            Assert.AreEqual("3", dataGrid[2, 0].Value);
            Assert.AreEqual("Item 5", dataGrid[2, 1].Value);
            Assert.AreEqual("Item 5", dataGrid[2, 1].FindTextBlock().Text);
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void SetValueWhenClickedOnce(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            Assert.AreEqual("1", cell.Value);

            cell.Click();
            cell.Value = "11";
            Assert.AreEqual("11", cell.Value);
        }

        [TestCase("DataGrid")]
        [TestCase("DataGrid10")]
        [TestCase("DataGridNoHeaders")]
        [TestCase("TemplateColumnDataGrid")]
        public void SetValueWhenClickedTwice(string name)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            Assert.AreEqual("1", cell.Value);

            cell.Click();
            cell.Click();
            cell.Value = "11";
            Assert.AreEqual("11", cell.Value);
        }

        [Explicit("Dunno if this is possible.")]
        [TestCase("DataGrid")]
        [TestCase("SelectCellDataGrid")]
        public void SetInvalidValueThrows(string name)
        {
            Assert.Inconclusive("VS test runner does not understand [Explicit].");
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);

            var cell = dataGrid[0, 0];
            var exception = Assert.Throws<InvalidOperationException>(() => cell.Value = "a");
            Assert.AreEqual("Failed setting value.", exception.Message);
        }

        [Test]
        public void SetValueUpdatesBinding()
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid("DataGrid");
            var readOnly = window.FindDataGrid("ReadOnlyDataGrid");

            Assert.AreEqual("1", dataGrid[0, 0].Value);
            Assert.AreEqual("1", readOnly[0, 0].Value);

            dataGrid[0, 0].Value = "11";
            Assert.AreEqual("11", dataGrid[0, 0].Value);
            Assert.Inconclusive("Figure out the least ugly way here.");
            //// ReSharper disable once HeuristicUnreachableCode
            Assert.AreEqual("11", readOnly[0, 0].Value);
        }

        [TestCase("DataGrid10", 9)]
        [TestCase("DataGrid10", 10)]
        public void SetValueWhenOffScreen(string name, int row)
        {
            using var app = Application.Launch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            dataGrid[row, 0].Value = "-1";
            dataGrid[row, 1].Value = "Item -1";
            Assert.AreEqual("-1", dataGrid[row, 0].Value);
            Assert.AreEqual("Item -1", dataGrid[row, 1].Value);
        }

        [TestCase("DataGrid10")]
        public void GetValueWhenOffScreen(string name)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "DataGridWindow");
            var window = app.MainWindow;
            var dataGrid = window.FindDataGrid(name);
            Assert.AreEqual("10", dataGrid[9, 0].Value);
            Assert.AreEqual("Item 10", dataGrid[9, 1].Value);
        }
    }
}
