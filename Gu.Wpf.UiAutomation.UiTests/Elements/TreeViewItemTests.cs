namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class TreeViewItemTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void FromAutomationElement()
        {
            using var app = Application.Launch(ExeFileName, "TreeViewWindow");
            var window = app.MainWindow;
            var treeViewItem = window.FindTreeView().Items[0];
            Assert.IsInstanceOf<TreeViewItem>(UiElement.FromAutomationElement(treeViewItem.AutomationElement));
        }
    }
}