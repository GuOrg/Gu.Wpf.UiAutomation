namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class TreeViewTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void SelectionTest()
        {
            using (var app = Application.Launch(ExeFileName, "TreeViewWindow"))
            {
                var window = app.MainWindow();
                var tree = window.FindTreeView();
                Assert.IsNull(tree.SelectedTreeViewItem);
                Assert.AreEqual(2, tree.Items.Count);
                var treeItem = tree.Items[0];
                treeItem.Expand();
                treeItem.Items[1].Expand();
                treeItem.Items[1].Items[0].Select();
                Assert.NotNull(tree.SelectedTreeViewItem);
                Assert.AreEqual("Lvl3 a", tree.SelectedTreeViewItem.Text);
            }
        }
    }
}
