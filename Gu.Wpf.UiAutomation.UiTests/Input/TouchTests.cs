namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System;
    using System.Linq;
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
                app.MainWindow.FindButton("Clear").Click();
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
                    "TouchEnter Position: 99,299",
                    "PreviewTouchDown Position: 99,299",
                    "TouchDown Position: 99,299",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchUp Position: 99,299",
                    "TouchUp Position: 99,299",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: 99,299",
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
                using (Touch.Down(area.Bounds.Center()))
                {
                    Touch.DragTo(area.Bounds.TopLeft);
                }

                var expected = new[]
                {
                    "TouchEnter Position: 99,299",
                    "PreviewTouchDown Position: 99,299",
                    "TouchDown Position: 99,299",
                    "ManipulationStarting",
                    "ManipulationStarted",
                    "PreviewTouchMove Position: -1,-1",
                    "TouchMove Position: -1,-1",
                    "ManipulationDelta",
                    "PreviewTouchUp Position: -1,-1",
                    "TouchUp Position: -1,-1",
                    "ManipulationInertiaStarting",
                    "ManipulationCompleted",
                    "TouchLeave Position: -1,-1",
                };

                CollectionAssert.AreEqual(expected, events.Items.Select(x => x.Text).ToArray());
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
