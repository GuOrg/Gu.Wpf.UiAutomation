namespace Gu.Wpf.UiAutomation.UITests
{
    using System.IO;
    using NUnit.Framework;

    public class ImageAssertTests
    {
        private static readonly string ExeFileName = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            @"..\..\TestApplications\WpfApplication\bin\WpfApplication.exe");

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
                    ImageAssert.AreEqual(ImageFileName, button);
                }
                catch
                {
                    ScreenCapture.CaptureToFile(button, Path.Combine(Path.GetTempPath(), "button.png"));
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