namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class ScrollItemPatternTests : UITestBase
    {
        private AutomationElement grid;

        public ScrollItemPatternTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [OneTimeSetUp]
        public void SelectTab()
        {
            var mainWindow = this.App.GetMainWindow(this.Automation);
            var tab = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tab)).AsTab();
            tab.SelectTabItem(1);
            this.grid = tab.FindFirstDescendant(cf => cf.ByAutomationId("LargeListView"));
        }

        [Test]
        public void Test()
        {
            var grid = this.grid;
            Assert.That(grid, Is.Not.Null);
            var gridPattern = this.grid.Patterns.Grid.Pattern;
            Assert.That(gridPattern, Is.Not.Null);
            Assert.That(gridPattern.ColumnCount.Value, Is.EqualTo(2));
            Assert.That(gridPattern.RowCount.Value, Is.EqualTo(7));
            ItemRealizer.RealizeItems(grid);
            var items = grid.AsGrid().Rows;
            Assert.That(items, Has.Length.EqualTo(gridPattern.RowCount.Value));
            var scrollPattern = grid.Patterns.Scroll.Pattern;
            Assert.That(scrollPattern, Is.Not.Null);
            Assert.That(scrollPattern.VerticalScrollPercent.Value, Is.EqualTo(0));
            foreach (var item in items)
            {
                var scrollItemPattern = item.Patterns.ScrollItem.Pattern;
                Assert.That(scrollItemPattern, Is.Not.Null);
                item.ScrollIntoView();
            }

            Assert.That(scrollPattern.VerticalScrollPercent.Value, Is.GreaterThan(0));
        }
    }
}
