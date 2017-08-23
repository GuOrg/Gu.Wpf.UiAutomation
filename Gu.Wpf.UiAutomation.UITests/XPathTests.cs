namespace Gu.Wpf.UiAutomation.UITests
{
    using Gu.Wpf.UiAutomation.UIA3;
    using NUnit.Framework;

    [TestFixture]
    public class XPathTests
    {
        [Test]
        public void NotepadFindFirst()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.GetMainWindow();
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
                var file = window.FindFirstByXPath($"/MenuBar/MenuItem[@Name='{this.GetFileMenuText()}']");
                Assert.That(file, Is.Not.Null);
            }
        }

        [Test]
        public void NotePadFindAll()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.GetMainWindow();
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
                var items = window.FindAllByXPath("//MenuItem");
                Assert.That(items, Is.Not.Null);
                Assert.That(items, Has.Length.EqualTo(6));
            }
        }

        [Test]
        public void NotePadFindAllIndexed()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.GetMainWindow();
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
                var items = window.FindAllByXPath("(//MenuBar)[1]/MenuItem");
                Assert.That(items, Is.Not.Null);
                Assert.That(items, Has.Length.EqualTo(1));
                items = window.FindAllByXPath("(//MenuBar)[2]/MenuItem");
                Assert.That(items, Is.Not.Null);
                Assert.That(items, Has.Length.EqualTo(5));
            }
        }

        private string GetFileMenuText()
        {
            switch (SystemLanguageRetreiver.GetCurrentOsCulture().TwoLetterISOLanguageName)
            {
                case "de":
                    return "Datei";
                default:
                    return "File";
            }
        }
    }
}
