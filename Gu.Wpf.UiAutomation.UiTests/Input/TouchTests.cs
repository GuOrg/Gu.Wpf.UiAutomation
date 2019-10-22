namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    public class TouchTests
    {
        private const string ExeFileName = "WpfApplication.exe";
        private const string WindowName = "TouchWindow";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Touch must be initialized before the app to test touch on is started.
            // Not sure why but my guess is the call to InitializeTouchInjection adds a touch device making WPF start listening for touch input.
            Touch.Initialize();
        }

        [SetUp]
        public void SetUp()
        {
            if (WindowsVersion.IsAppVeyor())
            {
                Assert.Inconclusive("We need a Win 10 image on AppVeyor for testing touch.");
            }

            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                window.WaitUntilResponsive();
                window.FindButton("Clear").Invoke();
                window.WaitUntilResponsive();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void Tap()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                Touch.Tap(area.Bounds.Center());
                var expected = WindowsVersion.IsWindows10()
                    ? new[]
                    {
                        "TouchEnter Position: 250,300",
                        "PreviewTouchDown Position: 250,300",
                        "TouchDown Position: 250,300",
                        "ManipulationStarting",
                        "ManipulationStarted",
                        "PreviewTouchUp Position: 250,300",
                        "TouchUp Position: 250,300",
                        "ManipulationInertiaStarting",
                        "ManipulationCompleted",
                        "TouchLeave Position: 250,300",
                    }
                    : new[]
                    {
                        "TouchEnter Position: 249,299", "PreviewTouchDown Position: 249,299",
                        "TouchDown Position: 249,299", "ManipulationStarting", "ManipulationStarted",
                        "PreviewTouchUp Position: 249,299", "TouchUp Position: 249,299",
                        "ManipulationInertiaStarting", "ManipulationCompleted", "TouchLeave Position: 249,299",
                    };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
            }
        }

        [Test]
        public void Drag()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");

                Touch.Drag(area.Bounds.Center(), area.Bounds.Center() + new Vector(10, 10));
                var expected = new[]
                {
                    "TouchEnter Position: 250,300",
                    "PreviewTouchDown Position: 250,300",
                    "TouchDown Position: 250,300",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchMove Position: 260,310",
                    "TouchMove Position: 260,310",
                    "ManipulationDelta",
                    "PreviewTouchUp Position: 260,310",
                    "TouchUp Position: 260,310",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 260,310",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
            }
        }

        [TestCase(0)]
        [TestCase(200)]
        public void DragWithDuration(int milliseconds)
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");

                Touch.Drag(area.Bounds.Center(), area.Bounds.Center() + new Vector(10, 10), TimeSpan.FromMilliseconds(milliseconds));

                var expected = new[]
                {
                    "TouchEnter Position: 250,300",
                    "PreviewTouchDown Position: 250,300",
                    "TouchDown Position: 250,300",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchMove Position: 260,310",
                    "TouchMove Position: 260,310",
                    "ManipulationDelta",
                    "PreviewTouchUp Position: 260,310",
                    "TouchUp Position: 260,310",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 260,310",
                };
                if (milliseconds == 0)
                {
                    CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
                }
                else
                {
                    // CollectionAssert.IsSubsetOf(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
                }
            }
        }

        [Test]
        public void DragTo()
        {
            if (WindowsVersion.IsAppVeyor())
            {
                Assert.Inconclusive("We need a Win 10 image on AppVeyor for testing touch.");
            }

            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                using (Touch.Hold(area.Bounds.Center()))
                {
                    Touch.DragTo(area.Bounds.Center() + new Vector(10, 10));
                }

                var expected = new[]
                {
                    "TouchEnter Position: 250,300",
                    "PreviewTouchDown Position: 250,300",
                    "TouchDown Position: 250,300",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchMove Position: 250,300",
                    "TouchMove Position: 250,300",
                    "PreviewTouchMove Position: 260,310",
                    "TouchMove Position: 260,310",
                    "ManipulationDelta",
                    "PreviewTouchUp Position: 260,310",
                    "TouchUp Position: 260,310",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 260,310",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
            }
        }

        [Test]
        public void TwoFingers()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                var cp = area.Bounds.Center();
                Touch.Multi(
                    new TwoFingers(cp + new Vector(-100, 0), cp + new Vector(100, 0)),
                    new TwoFingers(cp + new Vector(-50, 0), cp + new Vector(50, 0)),
                    TimeSpan.FromMilliseconds(10));
                var expected = WindowsVersion.IsWindows10()
                ? new[]
                {
                    "TouchEnter Position: 150,300",
                    "PreviewTouchDown Position: 150,300",
                    "TouchDown Position: 150,300",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "TouchEnter Position: 350,300",
                    "PreviewTouchDown Position: 350,300",
                    "TouchDown Position: 350,300",
                    "ManipulationDelta",
                    "PreviewTouchMove Position: 200,300",
                    "TouchMove Position: 200,300",
                    "PreviewTouchMove Position: 300,300",
                    "TouchMove Position: 300,300",
                    "PreviewTouchUp Position: 200,300",
                    "TouchUp Position: 200,300",
                    "TouchLeave Position: 200,300",
                    "PreviewTouchUp Position: 300,300",
                    "TouchUp Position: 300,300",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 300,300",
                }
                : new[]
                {
                    "TouchEnter Position: 149,299",
                    "PreviewTouchDown Position: 149,299",
                    "TouchDown Position: 149,299",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "TouchEnter Position: 349,299",
                    "PreviewTouchDown Position: 349,299",
                    "TouchDown Position: 349,299",
                    "ManipulationDelta",
                    "PreviewTouchMove Position: 199,299",
                    "TouchMove Position: 199,299",
                    "PreviewTouchMove Position: 299,299",
                    "TouchMove Position: 299,299",
                    "PreviewTouchUp Position: 199,299",
                    "TouchUp Position: 199,299",
                    "TouchLeave Position: 199,299",
                    "PreviewTouchUp Position: 299,299",
                    "TouchUp Position: 299,299",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 299,299",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
            }
        }

        [Test]
        public void Pinch()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                var cp = area.Bounds.Center();
                Touch.Pinch(cp, 100, 50, TimeSpan.FromMilliseconds(10));
                var expected = WindowsVersion.IsWindows10()
                ? new[]
                {
                    "TouchEnter Position: 320,370",
                    "PreviewTouchDown Position: 320,370",
                    "TouchDown Position: 320,370",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "TouchEnter Position: 178,229",
                    "PreviewTouchDown Position: 178,229",
                    "TouchDown Position: 178,229",
                    "ManipulationDelta",
                    "PreviewTouchMove Position: 285,335",
                    "TouchMove Position: 285,335",
                    "PreviewTouchMove Position: 213,264",
                    "TouchMove Position: 213,264",
                    "PreviewTouchUp Position: 285,335",
                    "TouchUp Position: 285,335",
                    "TouchLeave Position: 285,335",
                    "PreviewTouchUp Position: 213,264",
                    "TouchUp Position: 213,264",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 213,264",
                }
                : new[]
                {
                    "TouchEnter Position: 319,369",
                    "PreviewTouchDown Position: 319,369",
                    "TouchDown Position: 319,369",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "TouchEnter Position: 178,228",
                    "PreviewTouchDown Position: 178,228",
                    "TouchDown Position: 178,228",
                    "ManipulationDelta",
                    "PreviewTouchMove Position: 284,334",
                    "TouchMove Position: 284,334",
                    "PreviewTouchMove Position: 213,263",
                    "TouchMove Position: 213,263",
                    "PreviewTouchUp Position: 284,334",
                    "TouchUp Position: 284,334",
                    "TouchLeave Position: 284,334",
                    "PreviewTouchUp Position: 213,263",
                    "TouchUp Position: 213,263",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 213,263",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);
            }
        }

        [Test]
        public void TapThenClickWithMove()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                Touch.Tap(area.Bounds.Center());
                var expected = WindowsVersion.IsWindows10()
                ? new[]
                {
                    "TouchEnter Position: 250,300",
                    "PreviewTouchDown Position: 250,300",
                    "TouchDown Position: 250,300",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchUp Position: 250,300",
                    "TouchUp Position: 250,300",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 250,300",
                }
                : new[]
                {
                    "TouchEnter Position: 249,299",
                    "PreviewTouchDown Position: 249,299",
                    "TouchDown Position: 249,299",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchUp Position: 249,299",
                    "TouchUp Position: 249,299",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 249,299",
                };
                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);

                app.MainWindow.FindButton("Clear").Click(moveMouse: true);
                CollectionAssert.IsEmpty(events.Items);
                Assert.AreEqual(CursorState.CURSOR_SHOWING, Mouse.GetCursorState());
            }
        }

        [Test]
        public void TapThenClickNoMove()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                Touch.Tap(area.Bounds.Center());
                var expected = WindowsVersion.IsWindows10()
                ? new[]
                {
                    "TouchEnter Position: 250,300",
                    "PreviewTouchDown Position: 250,300",
                    "TouchDown Position: 250,300",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchUp Position: 250,300",
                    "TouchUp Position: 250,300",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 250,300",
                }
                : new[]
                {
                    "TouchEnter Position: 249,299",
                    "PreviewTouchDown Position: 249,299",
                    "TouchDown Position: 249,299",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchUp Position: 249,299",
                    "TouchUp Position: 249,299",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 249,299",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray(), EventStringComparer.Default);

                app.MainWindow.FindButton("Clear").Click();
                CollectionAssert.IsEmpty(events.Items);
                Assert.AreEqual(CursorState.CURSOR_SHOWING, Mouse.GetCursorState());
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static void Dump(ListBox events)
        {
            foreach (var item in events.Items)
            {
                Console.WriteLine($"\"{item.Text}\",");
            }
        }

        private class EventStringComparer : IComparer
        {
            internal static readonly IComparer Default = new EventStringComparer();

            public int Compare(object x, object y) => Compare((string)x, (string)y);

            private static int Compare(string x, string y)
            {
                var i = x.IndexOf(' ');
                if (i < 0)
                {
                    return x == y ? 0 : 1;
                }

                const string Pattern = "(?<event>\\w+) Position: (?<x>\\d+),(?<y>\\d)";
                var mx = Regex.Match(x, Pattern);
                var my = Regex.Match(y, Pattern);
                if (mx.Success && my.Success)
                {
                    if (StringEquals(mx, my, "event") &&
                        IntEquals(mx, my, "x", 1) &&
                        IntEquals(mx, my, "y", 1))
                    {
                        return 0;
                    }
                }

                return -1;

                static bool StringEquals(Match x, Match y, string name)
                {
                    return x.Groups[name].Value == y.Groups[name].Value;
                }

                static bool IntEquals(Match x, Match y, string name, int tolerance)
                {
                    return int.TryParse(x.Groups[name].Value, out var xi) &&
                           int.TryParse(y.Groups[name].Value, out var yi) &&
                           Math.Abs(xi - yi) <= tolerance;
                }
            }
        }
    }
}
