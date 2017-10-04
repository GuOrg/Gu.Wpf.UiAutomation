namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using NUnit.Framework;

    public class MenuTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void TestMenuWithSubMenus()
        {
            using (var app = Application.Launch(ExeFileName, "MenuWindow"))
            {
                var window = app.MainWindow;
                var menu = window.FindMenu();
                Assert.That(menu, Is.Not.Null);
                var items = menu.Items;
                Assert.AreEqual(2, items.Count);
                Assert.AreEqual("File", items[0].Text);
                Assert.AreEqual("Edit", items[1].Text);

                var subitems1 = items[0].Items;
                Assert.AreEqual(1, subitems1.Count);
                Assert.AreEqual("Exit", subitems1[0].Text);

                var subitems2 = items[1].Items;
                Assert.AreEqual(2, subitems2.Count);
                Assert.AreEqual("Copy", subitems2[0].Text);
                Assert.AreEqual("Paste", subitems2[1].Text);

                var subsubitems1 = subitems2[0].Items;
                Assert.AreEqual(2, subsubitems1.Count);
                Assert.AreEqual("Plain", subsubitems1[0].Text);
                Assert.AreEqual("Fancy", subsubitems1[1].Text);
            }
        }

        [Test]
        public void TestMenuWithSubMenusByName()
        {
            using (var app = Application.Launch(ExeFileName, "MenuWindow"))
            {
                var window = app.MainWindow;
                var menu = window.FindMenu();
                var edit = menu.Items["Edit"];
                Assert.AreEqual("Edit", edit.Text);

                var copy = edit.Items["Copy"];
                Assert.AreEqual("Copy", copy.Text);

                var fancy = copy.Items["Fancy"];
                Assert.AreEqual("Fancy", fancy.Text);
            }
        }
    }
}
