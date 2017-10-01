namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class ImageAssertTests
    {
        private static readonly string ExeFileName = Application.FindExe("WpfApplication.exe");

        [SetUp]
        public void SetUp()
        {
            ImageAssert.OnFail = OnFail.DoNothing;
        }

        [Test]
        public void WhenEqualExplicitPath()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("SizeButton");
                try
                {
                    // Not sure why this pause is needed on win 10.
                    Wait.For(TimeSpan.FromMilliseconds(200));
                    var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
                    ImageAssert.AreEqual(fileName, button);
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
        public void WhenEqualRelativePath()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("SizeButton");
                try
                {
                    // Not sure why this pause is needed on win 10.
                    Wait.For(TimeSpan.FromMilliseconds(200));
                    ImageAssert.AreEqual(@".\Images\button.png", button);
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
        public void WhenEqualResourceName()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("SizeButton");
                try
                {
                    // Not sure why this pause is needed on win 10.
                    Wait.For(TimeSpan.FromMilliseconds(200));
                    ImageAssert.AreEqual(@"button_resource", button);
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
        public void WhenEqualResource()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var button = window.FindButton("SizeButton");
                try
                {
                    // Not sure why this pause is needed on win 10.
                    Wait.For(TimeSpan.FromMilliseconds(200));
                    ImageAssert.AreEqual(Properties.Resources.button_resource, button);
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
                var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
                Assert.Throws<NUnit.Framework.AssertionException>(() => ImageAssert.AreEqual(fileName, window));
            }
        }

        [Test]
        public void WhenNotEqualSaveFileToTempRootedPath()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
                var tempFile = Path.Combine(Path.GetTempPath(), "button.png");
                File.Delete(tempFile);
                ImageAssert.OnFail = OnFail.SaveImageToTemp;
                Assert.Throws<NUnit.Framework.AssertionException>(() => ImageAssert.AreEqual(fileName, window));
                Assert.AreEqual(true, File.Exists(tempFile));
            }
        }

        [TestCase(@".\Images\button.png")]
        [TestCase(@"Images\button.png")]
        public void WhenNotEqualSaveFileToTempRelativePath(string fileName)
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var tempFile = Path.Combine(Path.GetTempPath(), "button.png");
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }

                ImageAssert.OnFail = OnFail.SaveImageToTemp;
                Assert.Throws<NUnit.Framework.AssertionException>(() => ImageAssert.AreEqual(fileName, window));
                Assert.AreEqual(true, File.Exists(tempFile));
            }
        }

        [TestCase("button_resource")]
        public void WhenNotEqualSaveFileToResourceName(string fileName)
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var tempFile = Path.Combine(Path.GetTempPath(), "button_resource.png");
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }

                ImageAssert.OnFail = OnFail.SaveImageToTemp;
                Assert.Throws<NUnit.Framework.AssertionException>(() => ImageAssert.AreEqual(fileName, window));
                Assert.AreEqual(true, File.Exists(tempFile));
            }
        }

        [Test]
        public void WhenNotEqualWithAction()
        {
            using (var app = Application.Launch(ExeFileName, "SizeWindow"))
            {
                var window = app.MainWindow;
                var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
                var count = 0;
                Assert.Throws<NUnit.Framework.AssertionException>(() => ImageAssert.AreEqual(fileName, window, (exception, bitmap) => count++));
                Assert.AreEqual(1, count);
            }
        }
    }
}