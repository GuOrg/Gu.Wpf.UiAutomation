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

        [SetUp]
        public void SetUp()
        {
            Touch.Initialize();
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
