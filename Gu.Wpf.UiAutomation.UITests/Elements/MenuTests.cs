namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using NUnit.Framework;

    public class MenuTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

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
                Assert.AreEqual("File", items[0].Properties.Name);
                Assert.AreEqual("Edit", items[1].Properties.Name);

                var subitems1 = items[0].Items;
                Assert.AreEqual(1, subitems1.Count);
                Assert.AreEqual("Exit", subitems1[0].Properties.Name);

                var subitems2 = items[1].Items;
                Assert.AreEqual(2, subitems2.Count);
                Assert.AreEqual("Copy", subitems2[0].Properties.Name);
                Assert.AreEqual("Paste", subitems2[1].Properties.Name);

                var subsubitems1 = subitems2[0].Items;
                Assert.AreEqual(2, subsubitems1.Count);
                Assert.AreEqual("Plain", subsubitems1[0].Properties.Name);
                Assert.AreEqual("Fancy", subsubitems1[1].Properties.Name);
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
                Assert.AreEqual("Edit", edit.Properties.Name.Value);

                var copy = edit.Items["Copy"];
                Assert.AreEqual("Copy", copy.Properties.Name.Value);

                var fancy = copy.Items["Fancy"];
                Assert.AreEqual("Fancy", fancy.Properties.Name.Value);
            }
        }
    }
}
