namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System.Windows;
    using NUnit.Framework;

    public class TouchBox
    {
        [Test]
        public void Tap()
        {
            using (var app = Application.AttachOrLaunch("WpfApplication.exe", "EmptyWindow"))
            {
                var window = app.MainWindow;
                Touch.Tap(window.Bounds.Center());
            }
        }

        [Test]
        public void TapThenOut()
        {
            using (var app = Application.AttachOrLaunch("WpfApplication.exe", "EmptyWindow"))
            {
                var window = app.MainWindow;
                Touch.Tap(window.Bounds.Center());
                Mouse.Restore();
                Mouse.Position = new Point(0, 0);
            }
        }

        [Test]
        public void TapThen()
        {
            using (var app = Application.AttachOrLaunch("WpfApplication.exe", "TouchWindow"))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                Touch.Tap(area.Bounds.Center());
                Mouse.Position = app.MainWindow.FindButton("Clear").Bounds.Center();
            }
        }

        [Test]
        public void TapClearThen()
        {
            using (var app = Application.AttachOrLaunch("WpfApplication.exe", "TouchWindow"))
            {
                var window = app.MainWindow;
                Touch.Tap(app.MainWindow.FindButton("Clear").Bounds.Center());
            }
        }
    }
}
