namespace Gu.Wpf.UiAutomation.UiTests
{
    using System.Globalization;
    using NUnit.Framework;

    [TestFixture]
    public class XPathTests
    {
        [Test]
        public void NotepadFindFirst()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.MainWindow;
                Assert.That(window.Title, Is.Not.Null);
                var file = window.FindFirstByXPath($"/MenuBar/MenuItem[@Name='{this.GetFileMenuText()}']");
                Assert.NotNull(file);
            }
        }

        [Test]
        public void NotePadFindAll()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.MainWindow;
                Assert.NotNull(window);
                Assert.NotNull(window.Title);
                var items = window.FindAllByXPath("//MenuItem");
                Assert.AreEqual(6, items.Count);
            }
        }

        [Test]
        public void NotePadFindAllIndexed()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.MainWindow;
                Assert.NotNull(window);
                Assert.NotNull(window.Title);
                var items = window.FindAllByXPath("(//MenuBar)[1]/MenuItem");
                Assert.AreEqual(1, items.Count);
                items = window.FindAllByXPath("(//MenuBar)[2]/MenuItem");
                Assert.AreEqual(5, items.Count);
            }
        }

        private string GetFileMenuText()
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
