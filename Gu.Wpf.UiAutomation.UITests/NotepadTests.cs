namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Diagnostics;
    using NUnit.Framework;

    [TestFixture]
    public class NotepadTests
    {
        [Test]
        public void NotepadLaunchTest()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.MainWindow();
                Assert.That(window, Is.Not.Null);
                Assert.That(window.Title, Is.Not.Null);
            }
        }

        [Test]
        public void NotepadAttachByNameTest()
        {
            using (Application.Launch("notepad.exe"))
            {
                using (var app = Application.Attach("notepad.exe"))
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    Assert.That(window.Title, Is.Not.Null);
                }
            }
        }

        [Test]
        public void NotepadAttachByProcessIdTest()
        {
            using (var launchedApp = Application.Launch("notepad.exe"))
            {
                using (var app = Application.Attach(launchedApp.ProcessId))
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    Assert.That(window.Title, Is.Not.Null);
                }
            }
        }

        [TestCase(@"C:\WINDOWS\system32\notepad.exe")]
        [TestCase("notepad.exe")]
        public void NotepadAttachOrLauchIdTest(string name)
        {
            using (Application.Launch("notepad.exe"))
            {
                using (var app = Application.AttachOrLaunch(new ProcessStartInfo(name)))
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    Assert.That(window.Title, Is.Not.Null);
                }
            }
        }

        [Test]
        public void NotepadAttachWithAbsoluteExePath()
        {
            using (Application.Launch("notepad.exe"))
            {
                using (var app = Application.Attach(@"C:\WINDOWS\system32\notepad.exe"))
                {
                    var window = app.MainWindow();
                    Assert.That(window, Is.Not.Null);
                    Assert.That(window.Title, Is.Not.Null);
                }
            }
        }
    }
}
