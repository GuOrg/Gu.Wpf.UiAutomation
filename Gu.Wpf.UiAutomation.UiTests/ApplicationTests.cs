namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using NUnit.Framework;

    public class ApplicationTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void DisposeWhenClosed()
        {
            using var app = Application.Launch("notepad.exe");
            Assert.AreEqual(false, app.Close());
        }

        [Test]
        public void Properties()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            Assert.AreEqual(app.MainWindow.NativeWindowHandle, app.MainWindowHandle);
            Assert.AreNotEqual(IntPtr.Zero, app.MainWindowHandle);
            Assert.NotZero(app.ProcessId);
            Assert.AreEqual("WpfApplication", app.Name);
            Assert.AreEqual(false, app.HasExited);
            Assert.AreEqual(true, app.Close());
            Assert.AreEqual(true, app.HasExited);
            Assert.AreEqual(0, app.ExitCode);
        }

        [Test]
        public void FindExe()
        {
            Assert.AreEqual(ExeFileName, Path.GetFileName(Application.FindExe(ExeFileName)));
        }

        [Test]
        public void KillLaunched()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Application.KillLaunched();
            Assert.Throws<InvalidOperationException>(() => app.WaitForMainWindow());
        }

        [Test]
        public void KillLaunchedExeFileName()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Application.KillLaunched(ExeFileName);
            Assert.Throws<InvalidOperationException>(() => app.WaitForMainWindow());
        }

        [Test]
        public void KillLaunchedExeFileNameAndArgs()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Application.KillLaunched(ExeFileName, "EmptyWindow");
            Assert.Throws<InvalidOperationException>(() => app.WaitForMainWindow());
        }

        [Test]
        public void Kill()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            app.Kill();
            Assert.AreEqual(-1, app.ExitCode);
        }

        [Test]
        public void GetAllTopLevelWindows()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            app.WaitForMainWindow();
            Assert.AreEqual(1, app.GetAllTopLevelWindows().Count);
        }

        [Test]
        public void GetMainWindowThrowsWithTimeOut()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SlowWindow");
            var exception = Assert.Throws<TimeoutException>(() => app.GetMainWindow(TimeSpan.FromMilliseconds(10)));
            Assert.AreEqual("Did not find Process.MainWindowHandle, if startup is slow try with a longer timeout.", exception.Message);
        }

        [Test]
        public void StartWaitForMainWindowAndClose()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow");
            var id = app.ProcessId;
            using var process = Process.GetProcessById(id);
            Assert.NotNull(process);
            Application.WaitForMainWindow(process);

            Application.KillLaunched(ExeFileName);
            Assert.Throws<ArgumentException>(() => Process.GetProcessById(id));
        }

        [Test]
        public void TryAttach()
        {
            using (Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                if (Application.TryAttach(ExeFileName, "EmptyWindow", out var app1))
                {
                    using (app1)
                    {
                        Assert.NotNull(app1.MainWindow);
                    }
                }
                else
                {
                    // Wrote it like this to see what the api looks like.
                    Assert.Fail("Failed to attach");
                }

                Assert.AreEqual(true, Application.TryAttach(new ProcessStartInfo(Application.FindExe(ExeFileName)) { Arguments = "EmptyWindow" }, out _));
                Assert.AreEqual(true, Application.TryAttach(new ProcessStartInfo(Application.FindExe(ExeFileName)) { Arguments = "EmptyWindow" }, OnDispose.LeaveOpen, out _));
                Assert.AreEqual(true, Application.TryAttach(ExeFileName, "EmptyWindow", OnDispose.LeaveOpen, out _));
                Assert.AreEqual(true, Application.TryAttach(ExeFileName, out _));
                Assert.AreEqual(true, Application.TryAttach(ExeFileName, OnDispose.LeaveOpen, out _));
                Assert.AreEqual(false, Application.TryAttach(new ProcessStartInfo(Application.FindExe(ExeFileName)) { Arguments = "MehWindow" }, out _));
            }
        }

        [Test]
        public void TryWithAttached()
        {
            using (Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                Assert.AreEqual(true, Application.TryWithAttached(ExeFileName, "EmptyWindow", app =>
                {
                    Assert.NotNull(app.MainWindow);
                }));

                Assert.AreEqual(true, Application.TryWithAttached(ExeFileName, app =>
                {
                    Assert.NotNull(app.MainWindow);
                }));

                Assert.AreEqual(true, Application.TryWithAttached(new ProcessStartInfo(ExeFileName), app =>
               {
                   Assert.NotNull(app.MainWindow);
               }));
            }
        }
    }
}
