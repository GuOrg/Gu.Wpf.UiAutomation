namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class PopupTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void CheckBoxInPopupTest()
        {
            using (var app = Application.Launch(ExeFileName, "PopupWindow"))
            {
                var window = app.MainWindow;
                var btn = window.FindToggleButton("PopupToggleButton1");
                btn.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.Popup;
                Assert.NotNull(popup);
                var popupChildren = popup.FindAllChildren();
                Assert.AreEqual(1, popupChildren.Count);
                var check = popupChildren[0].AsCheckBox();
                Assert.AreEqual("This is a popup", check.Text);
            }
        }

        [Test]
        public void MenuInPopupTest()
        {
            using (var app = Application.Launch(ExeFileName, "PopupWindow"))
            {
                var window = app.MainWindow;
                var btn = window.FindToggleButton("PopupToggleButton2");
                btn.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.Popup;
                var popupChildren = popup.FindAllChildren();
                Assert.AreEqual(1, popupChildren.Count);
                var menu = popupChildren[0].AsMenu();
                Assert.AreEqual(1, menu.Items.Count);
                var menuItem = menu.Items[0];
                Assert.AreEqual("Some MenuItem", menuItem.Text);
            }
        }
    }
}
