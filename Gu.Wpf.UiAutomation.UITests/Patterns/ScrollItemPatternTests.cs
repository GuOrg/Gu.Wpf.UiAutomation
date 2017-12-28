namespace Gu.Wpf.UiAutomation.UiTests.Patterns
{
    using NUnit.Framework;

    public class ScrollItemPatternTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Test()
        {
            using (var app = Application.Launch(ExeFileName, "LargeListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                Assert.That(listView, Is.Not.Null);
                var gridPattern = listView.AutomationElement.GridPattern();
                Assert.AreEqual(2, gridPattern.Current.ColumnCount);
                Assert.AreEqual(7, gridPattern.Current.RowCount);

                ItemRealizer.RealizeItems(listView);
                Assert.AreEqual(listView.Rows.Count, gridPattern.Current.RowCount);
                var scrollPattern = listView.AutomationElement.ScrollPattern();
                Assert.AreEqual(0, scrollPattern.Current.VerticalScrollPercent);
                foreach (var item in listView.Rows)
                {
                    var scrollItemPattern = item.AutomationElement.ScrollItemPattern();
                    Assert.NotNull(scrollItemPattern);
                    item.ScrollIntoView();
                }

                Assert.AreEqual(100, scrollPattern.Current.VerticalScrollPercent);
            }
        }
    }
}
