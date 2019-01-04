namespace Gu.Wpf.UiAutomation.UiTests.Elements
{
    using System.Globalization;
    using System.Linq;
    using NUnit.Framework;

    public class WindowTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void Close()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                window.Close();
            }
        }

        [Test]
        public void Dialog()
        {
            using (var app = Application.Launch(ExeFileName, "DialogWindow"))
            {
                var window = app.MainWindow;
                window.FindButton("Show Dialog").Click();
                var dialog = window.ModalWindows.Single();
                Assert.AreEqual("Message", dialog.FindTextBlock().Text);
                dialog.Close();
            }
        }

        [Test]
        public void Resize()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                Assert.AreEqual(true, window.CanResize);
                Assert.AreEqual("300,300", window.Bounds.Size.ToString(CultureInfo.InvariantCulture));

                window.Resize(270, 280);
                Assert.AreEqual(true, window.CanResize);
                Assert.AreEqual("270,280", window.Bounds.Size.ToString(CultureInfo.InvariantCulture));
            }
        }

        [Test]
        public void Move()
        {
            using (var app = Application.Launch(ExeFileName, "EmptyWindow"))
            {
                var window = app.MainWindow;
                Assert.AreEqual(true, window.CanMove);

                window.MoveTo(10, 20);
                Assert.AreEqual(true, window.CanMove);
                Assert.AreEqual("10,20,300,300", window.Bounds.ToString(CultureInfo.InvariantCulture));

                window.MoveTo(30, 40);
                Assert.AreEqual(true, window.CanMove);
                Assert.AreEqual("30,40,300,300", window.Bounds.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
