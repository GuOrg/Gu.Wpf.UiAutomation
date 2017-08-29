namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class WindowTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void ContextMenuTest()
        {
            using (var app = Application.Launch(ExeFileName))
            {
                var window = app.MainWindow();
                var btn = window.FindFirstDescendant(cf => cf.ByName("ContextMenu")).AsButton();
                Mouse.Click(MouseButton.Right, btn.GetClickablePoint());
                Wait.UntilInputIsProcessed();
                var ctxMenu = window.ContextMenu;
                Assert.That(ctxMenu, Is.Not.Null);
                var subMenuLevel1 = ctxMenu.Items;
                Assert.AreEqual(2, subMenuLevel1.Count);
                var subMenuLevel2 = subMenuLevel1[1].Items;
                Assert.AreEqual(1, subMenuLevel2.Count);
                var innerItem = subMenuLevel2[0];
                Assert.That(innerItem.Text, Is.EqualTo("Inner Context"));
            }
        }

        [Test]
        public void Close()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow();
                window.Close();
            }
        }

        [Test]
        public void MessageBox()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow();
                window.FindButton("Show MessageBox").Click();
                var dialog = window.FindMessageBox();
                Assert.AreEqual("Message", dialog.FindLabel().Text);
                dialog.Close();
            }
        }

        [Test]
        public void Dialog()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow();
                window.FindButton("Show Dialog").Click();
                var dialog = window.ModalWindows.Single();
                Assert.AreEqual("Message", dialog.FindTextBlock().Text);
                dialog.Close();
            }
        }
    }
}
