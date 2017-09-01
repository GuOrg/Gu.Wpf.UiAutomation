namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class ContextMenuTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void ContextMenuTest()
        {
            using (var app = Application.Launch(ExeFileName, "ContextMenuWindow"))
            {
                var window = app.MainWindow();
                var btn = window.FindButton("ContextMenu");
                btn.RightClick();
                Wait.For(TimeSpan.FromMilliseconds(100));
                var ctxMenu = window.ContextMenu;
                var subMenuLevel1 = ctxMenu.Items;
                Assert.AreEqual(2, subMenuLevel1.Count);
                var subMenuLevel2 = subMenuLevel1[1].Items;
                Assert.AreEqual(1, subMenuLevel2.Count);
                var innerItem = subMenuLevel2[0];
                Assert.AreEqual("Inner Context", innerItem.Text);
            }
        }
    }
}