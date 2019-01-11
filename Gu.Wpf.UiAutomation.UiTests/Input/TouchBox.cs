namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    public class TouchBox
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "TouchWindow";

        [Test]
        public void TapClear()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                Touch.Tap(app.MainWindow.FindButton("Clear").Bounds.Center());
            }
        }

        [Test]
        public void TapTouchArea()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                User32.GetCursorPos(out var p);
                Console.WriteLine(p.X);

                Touch.Tap(window.FindGroupBox("Touch area").Bounds.Center());
                User32.GetCursorPos(out p);
                Console.WriteLine(p.X);
            }
        }
    }
}
