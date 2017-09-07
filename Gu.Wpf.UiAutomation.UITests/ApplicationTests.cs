namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using NUnit.Framework;

    public class ApplicationTests
    {
        public static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [Test]
        public void DisposeWhenClosed()
        {
            using (var app = Application.Launch("notepad.exe"))
            {
                app.Close();
            }
        }

        [Test]
        public void StartWaitForMainWindowAndClose()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "EmptyWindow"))
            {
                var id = app.ProcessId;
                Assert.NotNull(Process.GetProcessById(id));
                Application.WaitForMainWindow(Process.GetProcessById(id));

                Application.KillLaunched(ExeFileName);
                Assert.Throws<ArgumentException>(() => Process.GetProcessById(id));
            }
        }
    }
}