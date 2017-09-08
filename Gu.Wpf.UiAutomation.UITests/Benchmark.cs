namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using NUnit.Framework;

    public class Benchmark
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName))
            {
                // for side effect of having the app open.
                var window = app.MainWindow;
                var checkBox = window.FindCheckBox("Test Checkbox");
                var isChecked = checkBox.IsChecked;
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void EmptyWindow()
        {
            var sw = Stopwatch.StartNew();
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var launch = sw.Elapsed;
                var window1 = app.MainWindow;
                var firstWindow = sw.Elapsed;
                var window2 = app.MainWindow;
                sw.Stop();
                Assert.NotNull(window1);
                Assert.AreSame(window1, window2);
                Console.WriteLine($"Launch:       {launch.TotalMilliseconds:F0} ms");
                Console.WriteLine($"MainWindow 1: {firstWindow.TotalMilliseconds - launch.TotalMilliseconds:F0} ms ({firstWindow.TotalMilliseconds:F0})");
                Console.WriteLine($"MainWindow 2: {sw.ElapsedMilliseconds - firstWindow.TotalMilliseconds:F0} ms   ({sw.ElapsedMilliseconds:F0})");
            }
        }

        [Test]
        public void CheckBox()
        {
            var sw = Stopwatch.StartNew();
            using (var app = Application.Launch(ExeFileName))
            {
                var launch = sw.Elapsed;
                var window = app.MainWindow;
                var mainWindow = sw.Elapsed;
                var checkBox = window.FindCheckBox("Test Checkbox");
                var findCheckBox = sw.Elapsed;
                var isChecked = checkBox.IsChecked;
                sw.Stop();
                Assert.AreEqual(false, isChecked);
                Console.WriteLine($"Launch:       {launch.TotalMilliseconds:F0} ms");
                Console.WriteLine($"MainWindow:   {mainWindow.TotalMilliseconds - launch.TotalMilliseconds:F0} ms ({mainWindow.TotalMilliseconds:F0})");
                Console.WriteLine($"FindCheckBox: {findCheckBox.TotalMilliseconds - mainWindow.TotalMilliseconds:F0} ms  ({findCheckBox.TotalMilliseconds:F0})");
                Console.WriteLine($"IsChecked:    {sw.ElapsedMilliseconds - findCheckBox.TotalMilliseconds:F0} ms   ({sw.ElapsedMilliseconds:F0})");
            }
        }

        [Test]
        public void AttachOrLaunchCheckBox()
        {
            var sw = Stopwatch.StartNew();
            using (var app = Application.AttachOrLaunch(ExeFileName))
            {
                var launch = sw.Elapsed;
                var window = app.MainWindow;
                var mainWindow = sw.Elapsed;
                var checkBox = window.FindCheckBox("Test Checkbox");
                var findCheckBox = sw.Elapsed;
                var isChecked = checkBox.IsChecked;
                sw.Stop();
                Assert.AreEqual(false, isChecked);
                Console.WriteLine($"AttachOrLaunch: {launch.TotalMilliseconds:F0} ms");
                Console.WriteLine($"MainWindow:     {mainWindow.TotalMilliseconds - launch.TotalMilliseconds:F0} ms  ({mainWindow.TotalMilliseconds:F0})");
                Console.WriteLine($"FindCheckBox:   {findCheckBox.TotalMilliseconds - mainWindow.TotalMilliseconds:F0} ms ({findCheckBox.TotalMilliseconds:F0})");
                Console.WriteLine($"IsChecked:      {sw.ElapsedMilliseconds - findCheckBox.TotalMilliseconds:F0} ms  ({sw.ElapsedMilliseconds:F0})");
            }
        }
    }
}