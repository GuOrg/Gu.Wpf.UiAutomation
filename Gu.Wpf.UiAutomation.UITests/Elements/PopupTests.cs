namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using NUnit.Framework;

    public class PopupTests : UITestBase
    {
        public PopupTests()
            : base(TestApplicationType.Wpf)
        {
        }

        [Test]
        public void CheckBoxInPopupTest()
        {
            var window = this.App.MainWindow();
            var btn = window.FindFirstDescendant(cf => cf.ByAutomationId("PopupToggleButton1"));
            btn.Click();
            Helpers.WaitUntilInputIsProcessed();
            var popup = window.Popup;
            Assert.That(popup, Is.Not.Null);
            var popupChildren = popup.FindAllChildren();
            Assert.That(popupChildren, Has.Length.EqualTo(1));
            var check = popupChildren[0].AsCheckBox();
            Assert.That(check.Text, Is.EqualTo("This is a popup"));
        }

        [Test]
        public void MenuInPopupTest()
        {
            var window = this.App.MainWindow();
            var btn = window.FindFirstDescendant(cf => cf.ByAutomationId("PopupToggleButton2"));
            btn.Click();
            Helpers.WaitUntilInputIsProcessed();
            var popup = window.Popup;
            Assert.That(popup, Is.Not.Null);
            var popupChildren = popup.FindAllChildren();
            Assert.That(popupChildren, Has.Length.EqualTo(1));
            var menu = popupChildren[0].AsMenu();
            Assert.That(menu.MenuItems, Has.Length.EqualTo(1));
            var menuItem = menu.MenuItems[0];
            Assert.That(menuItem.Text, Is.EqualTo("Some MenuItem"));
        }
    }
}
