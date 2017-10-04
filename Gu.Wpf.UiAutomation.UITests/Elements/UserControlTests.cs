namespace Gu.Wpf.UiAutomation.UITests.Elements
{
    using NUnit.Framework;

    public class UserControlTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [TestCase("UserControl1", "1")]
        [TestCase("UserControl2", "2")]
        public void Find(string key, string expected)
        {
            using (var app = Application.Launch(ExeFileName, "UserControlWindow"))
            {
                var window = app.MainWindow;
                var userControl = window.FindUserControl(key);
                Assert.AreEqual(expected, userControl.FindTextBlock().Text);
                Assert.AreEqual(expected, userControl.Content.AsTextBlock().Text);
            }
        }
    }
}