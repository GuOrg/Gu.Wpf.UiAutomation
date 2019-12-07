namespace Gu.Wpf.UiAutomation.UiTests.Extensions
{
    using System.Linq;
    using System.Windows.Automation;
    using NUnit.Framework;

    public class AutomationElementExtTests
    {
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched("WpfApplication.exe");
        }

        [Test]
        public void Parent()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var parent = window.AutomationElement.Parent();
            Assert.AreEqual("#32769", parent.ClassName());
            Assert.AreEqual(Desktop.AutomationElement, parent);

            var checkbox = window.AutomationElement.FindFirst(TreeScope.Descendants, Conditions.CheckBox);
            Assert.AreEqual(window.AutomationElement, checkbox.Parent());
        }

        [Test]
        public void TryFindFirstAncestorWindow()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var checkBox = window.FindTextBlock("TextBlock1");
            Assert.AreEqual(true, checkBox.AutomationElement.TryFindFirst(TreeScope.Ancestors, Conditions.Window, out var element));
            Assert.AreEqual(window.AutomationElement, element);
        }

        [Test]
        public void TryFindFirstCheckBox()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            Assert.AreEqual(true, window.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.CheckBox, out var element));
            Assert.AreEqual("CheckBox1Content", element.Name());
        }

        [Test]
        public void TryFindFirstTextBlock()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            Assert.AreEqual(true, window.AutomationElement.TryFindFirst(TreeScope.Children, Conditions.TextBlock, out var element));
            Assert.AreEqual("TextBlock1", element.Name());
        }

        [Test]
        public void TryFindFirstTextBlockAndNameWhenMissing()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            Assert.AreEqual(false, window.AutomationElement.TryFindFirst(TreeScope.Children, new AndCondition(Conditions.TextBlock, Conditions.ByName("missing")), out _));
        }

        [Test]
        public void FindAllTextBlockChildren()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var children = window.AutomationElement.FindAllChildren(Conditions.TextBlock);
            CollectionAssert.AreEqual(new[] { "TextBlock1", "TextBlock2" }, children.OfType<AutomationElement>().Select(x => x.Name()));
        }

        [Test]
        public void FindAllLabelChildren()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var children = window.AutomationElement.FindAllChildren(Conditions.Label);
            CollectionAssert.AreEqual(new[] { "Label1", "Label2" }, children.OfType<AutomationElement>().Select(x => x.Name()));
        }

        [Test]
        public void FindAllTextBoxChildren()
        {
            using var app = Application.AttachOrLaunch("WpfApplication.exe", "FindWindow");
            var window = app.MainWindow;
            var children = window.AutomationElement.FindAllChildren(Conditions.TextBox);
            CollectionAssert.AreEqual(new[] { "TextBox1", "TextBox2" }, children.OfType<AutomationElement>().Select(x => x.ValuePattern().Current.Value));
        }
    }
}
