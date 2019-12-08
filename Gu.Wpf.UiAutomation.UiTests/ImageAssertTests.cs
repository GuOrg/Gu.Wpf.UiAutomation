// ReSharper disable RedundantNameQualifier
namespace Gu.Wpf.UiAutomation.UiTests
{
    using System.IO;
    using NUnit.Framework;

    public class ImageAssertTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [SetUp]
        public void SetUp()
        {
            ImageAssert.OnFail = OnFail.DoNothing;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void WhenEqualExplicitPath()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
            ImageAssert.AreEqual(fileName, button);
        }

        [Test]
        public void WhenEqualRelativePath()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            ImageAssert.AreEqual(@".\Images\button.png", button);
        }

        [Test]
        public void WhenEqualResourceName()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            ImageAssert.AreEqual(@"button_resource", button);
        }

        [Test]
        public void WhenEqualResource()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            ImageAssert.AreEqual(Properties.Resources.button_resource, button);
        }

        [Test]
        public void WhenEqualBmp()
        {
            ImageAssert.AreEqual(Properties.Resources.SquareBmp, Properties.Resources.SquareBmp);
            ImageAssert.AreEqual(Properties.Resources.SquareBmp, Properties.Resources.SquareBmp, _ => { });
        }

        [Test]
        public void WhenEqualPng()
        {
            ImageAssert.AreEqual(Properties.Resources.SquarePng, Properties.Resources.SquarePng);
            ImageAssert.AreEqual(Properties.Resources.SquarePng, Properties.Resources.SquarePng, _ => { });
        }

        [Test]
        public void WhenEqualBmpPng()
        {
            ImageAssert.AreEqual(Properties.Resources.SquareBmp, Properties.Resources.SquarePng);
            ImageAssert.AreEqual(Properties.Resources.SquareBmp, Properties.Resources.SquarePng, _ => { });
            ImageAssert.AreEqual(Properties.Resources.SquarePng, Properties.Resources.SquareBmp);
            ImageAssert.AreEqual(Properties.Resources.SquarePng, Properties.Resources.SquareBmp, _ => { });
        }

        [Test]
        public void WhenNotEqualSize()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
            var exception = Assert.Throws<AssertException>(() => ImageAssert.AreEqual(fileName, window));
            string expected = "Images do not match.\r\n" +
                              "Expected width: 200 height: 100 pixel format: Format32bppArgb\r\n" +
                              "Actual   width: 300 height: 300 pixel format: Format32bppArgb\r\n";
            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void WhenNotEqualPixels()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button_wrong.png");
            var button = window.FindButton("SizeButton");
            var exception = Assert.Throws<AssertException>(() => ImageAssert.AreEqual(fileName, button));
            string expected = "Images do not match.\r\n" +
                              "Expected width: 200 height: 100 pixel format: Format32bppArgb\r\n" +
                              "Actual   width: 200 height: 100 pixel format: Format32bppArgb\r\n" +
                              "The following pixels are not matching:\r\n" +
                              "x    y    Expected Actual\r\n" +
                              "12   5    FF000000 FFE6E6FA\r\n" +
                              "139  69   FF000000 FFE6E6FA\r\n";
            Assert.AreEqual(expected, exception.Message);
        }

        [Test]
        public void WhenNotEqualSaveFileToTempRootedPath()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button_wrong.png");
            var tempFile = Path.Combine(Path.GetTempPath(), "button_wrong.png");
            File.Delete(tempFile);
            var button = window.FindButton("SizeButton");
            ImageAssert.OnFail = OnFail.SaveImageToTemp;
            Assert.Throws<AssertException>(() => ImageAssert.AreEqual(fileName, button));
            Assert.AreEqual(true, File.Exists(tempFile));
        }

        [TestCase(@".\Images\button_wrong.png")]
        [TestCase(@"Images\button_wrong.png")]
        public void WhenNotEqualSaveFileToTempRelativePath(string fileName)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var tempFile = Path.Combine(Path.GetTempPath(), "button_wrong.png");
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }

            var button = window.FindButton("SizeButton");
            ImageAssert.OnFail = OnFail.SaveImageToTemp;
            Assert.Throws<AssertException>(() => ImageAssert.AreEqual(fileName, button));
            Assert.AreEqual(true, File.Exists(tempFile));
        }

        [TestCase("button_resource")]
        public void WhenNotEqualSaveFileToResourceName(string fileName)
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var tempFile = Path.Combine(Path.GetTempPath(), "button_resource.png");
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }

            ImageAssert.OnFail = OnFail.SaveImageToTemp;
            Assert.Throws<AssertException>(() => ImageAssert.AreEqual(fileName, window));
            Assert.AreEqual(true, File.Exists(tempFile));
        }

        [Test]
        public void WhenNotEqualWithAction()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
            var count = 0;
            Assert.Throws<AssertException>(() => ImageAssert.AreEqual(fileName, window, (exception, bitmap) => count++));
            Assert.AreEqual(1, count);
        }

        [Test]
        public void WhenEqualElements()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            ImageAssert.AreEqual(button, button);
        }

        [Test]
        public void WhenNotEqualElements()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var button = window.FindButton("SizeButton");
            Assert.Throws<AssertException>(() => ImageAssert.AreEqual(window, button));
        }
    }
}
