namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class WindowTests : UITestBase
    {
        public WindowTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void ContextMenuTest()
        {
            this.RestartApp();
            var window = this.App.GetMainWindow(this.Automation);
            var btn = window.FindFirstDescendant(cf => cf.ByName("ContextMenu")).AsButton();
            Mouse.Click(MouseButton.Right, btn.GetClickablePoint());
            Helpers.WaitUntilInputIsProcessed();
            var ctxMenu = window.ContextMenu;
            Assert.That(ctxMenu, Is.Not.Null);
            var subMenuLevel1 = ctxMenu.MenuItems;
            Assert.That(subMenuLevel1, Has.Length.EqualTo(2));
            var subMenuLevel2 = subMenuLevel1[1].SubMenuItems;
            Assert.That(subMenuLevel2, Has.Length.EqualTo(1));
            var innerItem = subMenuLevel2[0];
            Assert.That(innerItem.Text, Is.EqualTo("Inner Context"));
        }
    }
}
