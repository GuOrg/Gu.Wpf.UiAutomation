namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using NUnit.Framework;

    public class TreeViewTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void SelectionTest()
        {
            using (var app = Application.Launch(ExeFileName, "TreeViewWindow"))
            {
                var window = app.MainWindow;
                var tree = window.FindTreeView();
                Assert.IsNull(tree.SelectedTreeViewItem);
                Assert.AreEqual(2, tree.Items.Count);
                var treeItem = tree.Items[0];
                treeItem.Expand();
                var item = treeItem.Items[1];
                item.Expand();
                item.Items[0].Select();
                Assert.AreEqual(true, item.Items[0].IsSelected);
                Assert.NotNull(tree.SelectedTreeViewItem);
                Assert.AreEqual("Lvl3 a", tree.SelectedTreeViewItem.Text);
            }
        }

        [Test]
        public void IsExpanded()
        {
            using (var app = Application.Launch(ExeFileName, "TreeViewWindow"))
            {
                var window = app.MainWindow;
                var tree = window.FindTreeView();
                var item = tree.Items[0];
                Assert.AreEqual(false, item.IsExpanded);

                item.IsExpanded = true;
                Assert.AreEqual(true, item.IsExpanded);

                item.IsExpanded = false;
                Assert.AreEqual(false, item.IsExpanded);
            }
        }
    }
}
