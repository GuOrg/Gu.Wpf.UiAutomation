namespace Gu.Wpf.UiAutomation.UITests.Patterns
{
    using System.IO;
    using NUnit.Framework;

    public class ScrollItemPatternTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void Test()
        {
            using (var app = Application.Launch(ExeFileName, "LargeListViewWindow"))
            {
                var window = app.MainWindow;
                var listView = window.FindListView();
                Assert.That(listView, Is.Not.Null);
                var gridPattern = listView.Patterns.Grid.Pattern;
                Assert.AreEqual(2, gridPattern.ColumnCount.Value);
                Assert.AreEqual(7, gridPattern.RowCount.Value);

                ItemRealizer.RealizeItems(listView);
                Assert.AreEqual(listView.Rows.Count, gridPattern.RowCount.Value);
                var scrollPattern = listView.Patterns.Scroll.Pattern;
                Assert.AreEqual(0, scrollPattern.VerticalScrollPercent.Value);
                foreach (var item in listView.Rows)
                {
                    var scrollItemPattern = item.Patterns.ScrollItem.Pattern;
                    Assert.NotNull(scrollItemPattern);
                    item.ScrollIntoView();
                }

                Assert.AreEqual(100, scrollPattern.VerticalScrollPercent.Value);
            }
        }
    }
}
