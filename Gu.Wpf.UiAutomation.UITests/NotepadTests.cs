namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Diagnostics;
    using NUnit.Framework;

    [TestFixture]
    public class NotepadTests
    {
        [Test]
        public void Launch()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                var window = app.MainWindow;
                Assert.NotNull(window);
                Assert.NotNull(window.Title);
            }
        }

        [Test]
        public void Attach()
        {
            using (Application.Launch("notepad.exe"))
            {
                using (var app = Application.Attach("notepad.exe"))
                {
                    var window = app.MainWindow;
                    Assert.NotNull(window);
                    Assert.NotNull(window.Title);
                }
            }
        }

        [Test]
        public void AttachProcessId()
        {
            using (var launchedApp = Application.Launch("notepad.exe"))
            {
                using (var app = Application.Attach(launchedApp.ProcessId))
                {
                    var window = app.MainWindow;
                    Assert.NotNull(window);
                    Assert.NotNull(window.Title);
                }
            }
        }

        [Test]
        public void AttachWithAbsoluteExePath()
        {
            using (Application.Launch("notepad.exe"))
            {
                using (var app = Application.Attach(@"C:\WINDOWS\system32\notepad.exe"))
                {
                    var window = app.MainWindow;
                    Assert.NotNull(window);
                    Assert.NotNull(window.Title);
                }
            }
        }

        [TestCase(@"C:\WINDOWS\system32\notepad.exe")]
        [TestCase("notepad.exe")]
        public void AttachOrLaunch(string name)
        {
            using (Application.Launch("notepad.exe"))
            {
                using (var app = Application.AttachOrLaunch(new ProcessStartInfo(name)))
                {
                    var window = app.MainWindow;
                    Assert.NotNull(window);
                    Assert.NotNull(window.Title);
                    app.Close();
                }
            }
        }
    }
}
