namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class ImageAssertTests
    {
        private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

        private static readonly string ImageFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"Images\button.png");

        [Test]
        public void WhenEqual()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("SizeButton");
                try
                {
                    // Not sure why this pause is needed on win 10.
                    Wait.For(TimeSpan.FromMilliseconds(200));
                    ImageAssert.AreEqual(ImageFileName, button);
                }
                catch
                {
                    Capture.ElementToFile(button, Path.Combine(Path.GetTempPath(), "button.png"));
                    Capture.ScreenToFile(Path.Combine(Path.GetTempPath(), "screen.png"));
                    throw;
                }
            }
        }

        [Test]
        public void WhenNotEqual()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                Assert.Throws<NUnit.Framework.AssertionException>(() => ImageAssert.AreEqual(ImageFileName, window));
            }
        }
    }
}