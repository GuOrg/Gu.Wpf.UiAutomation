namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    [TestFixture]
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
            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                Touch.Tap(app.MainWindow.FindButton("Clear").GetClickablePoint());
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
            if (WindowsVersion.IsAppVeyor())
            {
                Assert.Inconclusive("We need a Win 10 image on AppVeyor for testing touch.");
            }

            using (var app = Application.AttachOrLaunch(ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                var area = window.FindGroupBox("Touch area");
                var events = window.FindListBox("Events");
                Touch.Tap(area.Bounds.Center());
                var expected = new[]
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

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void Drag()
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

                Touch.Drag(area.Bounds.BottomRight, area.Bounds.BottomRight + new Vector(10, 10));
                var expected = new[]
                {
                    "TouchEnter Position: 499,599",
                    "PreviewTouchDown Position: 499,599",
                    "TouchDown Position: 499,599",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchMove Position: 499,599",
                    "TouchMove Position: 499,599",
                    "PreviewTouchMove Position: 509,609",
                    "TouchMove Position: 509,609",
                    "ManipulationDelta",
                    "PreviewTouchUp Position: 509,609",
                    "TouchUp Position: 509,609",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 509,609",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
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
                using (Touch.Down(area.Bounds.BottomRight))
                {
                    Touch.DragTo(area.Bounds.BottomRight + new Vector(10, 10));
                }

                var expected = new[]
                {
                    "TouchEnter Position: 499,599",
                    "PreviewTouchDown Position: 499,599",
                    "TouchDown Position: 499,599",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchMove Position: 499,599",
                    "TouchMove Position: 499,599",
                    "PreviewTouchMove Position: 509,609",
                    "TouchMove Position: 509,609",
                    "ManipulationDelta",
                    "PreviewTouchUp Position: 509,609",
                    "TouchUp Position: 509,609",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 509,609",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
            }
        }

        [Test]
        public void TapThenClick()
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
                Touch.Tap(area.Bounds.Center());
                var expected = new[]
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

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());

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
    }
}
