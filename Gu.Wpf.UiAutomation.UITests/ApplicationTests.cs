namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
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
            using (var process = Application.Start(ExeFileName, "EmptyWindow"))
            {
                Application.WaitForMainWindow(process);
                CollectionAssert.IsNotEmpty(Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ExeFileName)));

                Application.Kill(ExeFileName);
                Wait.For(TimeSpan.FromMilliseconds(100));
                CollectionAssert.IsEmpty(Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ExeFileName)));
            }
        }
    }
}