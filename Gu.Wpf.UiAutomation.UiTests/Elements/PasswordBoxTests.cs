namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using NUnit.Framework;

    public class PasswordBoxTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Find()
        {
            using (var app = Application.Launch(ExeFileName, "PasswordBoxWindow"))
            {
                var window = app.MainWindow;
                var passwordBox = window.FindPasswordBox();
                Assert.IsInstanceOf<PasswordBox>(UiElement.FromAutomationElement(passwordBox.AutomationElement));
            }
        }
    }
}