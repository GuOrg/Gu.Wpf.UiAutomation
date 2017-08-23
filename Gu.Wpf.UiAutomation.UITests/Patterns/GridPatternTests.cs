namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class GridPatternTests : UITestBase
    {
        private AutomationElement dataGrid;

        public GridPatternTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [OneTimeSetUp]
        public void SelectTab()
        {
            var mainWindow = this.App.GetMainWindow();
            var tab = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tab)).AsTab();
            var tabItem = tab.SelectTabItem(1);
            this.dataGrid = tabItem.FindFirstDescendant(cf => cf.ByAutomationId("dataGrid1"));
        }

        [Test]
        public void GridTest()
        {
            var dataGrid = this.dataGrid;
            Assert.That(dataGrid, Is.Not.Null);
            var gridPattern = dataGrid.Patterns.Grid.Pattern;
            Assert.That(gridPattern, Is.Not.Null);
            Assert.That(gridPattern.ColumnCount.Value, Is.EqualTo(2));
            Assert.That(gridPattern.RowCount.Value, Is.EqualTo(3));
            var item = gridPattern.GetItem(1, 1);
            Assert.That(item.Properties.Name.Value, Is.EqualTo("Patrick"));
        }
    }
}
