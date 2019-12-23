// ReSharper disable RedundantNameQualifier
namespace Gu.Wpf.UiAutomation.UiTests
{
    using System.IO;
    using NUnit.Framework;

    public class ImageAssertTests
    {
        private const string ExeFileName = "WpfApplication.exe";

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
        }

        [Test]
        public void WhenEqualPng()
        {
            ImageAssert.AreEqual(Properties.Resources.SquarePng, Properties.Resources.SquarePng);
        }

        [Test]
        public void WhenEqualBmpPng()
        {
            ImageAssert.AreEqual(Properties.Resources.SquareBmp, Properties.Resources.SquarePng);
            ImageAssert.AreEqual(Properties.Resources.SquarePng, Properties.Resources.SquareBmp);
        }

        [Test]
        public void WhenNotEqualSize()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button.png");
            var exception = Assert.Throws<ImageAssertException>(() => ImageAssert.AreEqual(fileName, window));
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
            var exception = Assert.Throws<ImageAssertException>(() => ImageAssert.AreEqual(fileName, button));
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
        public void WhenNotEqualWithAction()
        {
            using var app = Application.AttachOrLaunch(ExeFileName, "SizeWindow");
            var window = app.MainWindow;
            var fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Images\button_wrong.png");
            var button = window.FindButton("SizeButton");
            var count = 0;
            Assert.Throws<ImageAssertException>(() => ImageAssert.AreEqual(fileName, button, (expected, actual, resource) => count++));
            Assert.AreEqual(1, count);
        }
    }
}
