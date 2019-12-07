namespace Gu.Wpf.UiAutomation.UiTests
{
    using System.Globalization;
    using NUnit.Framework;

    [TestFixture]
    public class XPathTests
    {
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched("notepad.exe");
        }

        [Test]
        public void NotepadFindFirst()
        {
            using var app = Application.AttachOrLaunch("notepad.exe");
            var window = app.MainWindow;
            var item = window.FindFirstByXPath($"/MenuBar/MenuItem[@Name='{GetFileMenuText()}']");
            Assert.NotNull(item);
        }

        [Test]
        public void NotePadFindAll()
        {
            using var app = Application.AttachOrLaunch("notepad.exe");
            var window = app.MainWindow;
            var items = window.FindAllByXPath("//MenuItem");
            Assert.AreEqual(6, items.Count);
        }

        [Test]
        public void NotePadFindAllIndexed()
        {
            using var app = Application.AttachOrLaunch("notepad.exe");
            var window = app.MainWindow;
            var items = window.FindAllByXPath("(//MenuBar)[1]/MenuItem");
            Assert.AreEqual(1, items.Count);
            items = window.FindAllByXPath("(//MenuBar)[2]/MenuItem");
            Assert.AreEqual(5, items.Count);
        }

        private static string GetFileMenuText()
        {
            switch (CultureInfo.InstalledUICulture.TwoLetterISOLanguageName)
            {
                case "de":
                    return "Datei";
                default:
                    return "File";
            }
        }
    }
}
