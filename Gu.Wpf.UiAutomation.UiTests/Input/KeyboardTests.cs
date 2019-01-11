namespace Gu.Wpf.UiAutomation.UiTests.Input
{
    using System.Diagnostics;
    using System.Threading;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    public class KeyboardTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [Test]
        public void TypeKeysThenBackspace()
        {
            using (var app = Application.Launch(ExeFileName, "SingleTextBoxWindow"))
            {
                var window = app.MainWindow;
                Keyboard.Type("abc");
                Keyboard.Type(Key.BACK);
                var textBox = window.FindTextBox();
                window.WaitUntilResponsive();
                Assert.AreEqual("ab", textBox.Text);
            }
        }

        [TestCase(Key.KEY_Z, "z")]
        public void TypeKey(Key key, string expected)
        {
            using (var app = Application.Launch(ExeFileName, "SingleTextBoxWindow"))
            {
                var mainWindow = app.MainWindow;
                Keyboard.Type(key);
                var textBox = mainWindow.FindTextBox();
                Assert.AreEqual(expected, textBox.Text);
            }
        }

        [TestCase("abc")]
        [TestCase("ééééééööööö aaa | ")]
        [TestCase("ঋ ঌ এ ঐ ও ঔ ক খ গ ঘ ঙ চ ছ জ ঝ ঞ ট ঠ ড ঢ")]
        public void TypeString(string text)
        {
            using (var app = Application.Launch(ExeFileName, "SingleTextBoxWindow"))
            {
                var mainWindow = app.MainWindow;
                Keyboard.Type(text);
                var textBox = mainWindow.FindTextBox();
                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 1000)
                {
                    if (textBox.Text == text)
                    {
                        Assert.Pass();
                    }

                    Thread.Sleep(10);
                }

                Assert.AreEqual(text, textBox.Text);
            }
        }

        [TestCase(Key.KEY_Z, "Z")]
        public void TypeKeyWhilePressingShift(Key key, string expected)
        {
            using (var app = Application.Launch(ExeFileName, "SingleTextBoxWindow"))
            {
                var mainWindow = app.MainWindow;
                using (Keyboard.Pressing(Key.SHIFT))
                {
                    Keyboard.Type(key);
                }

                var textBox = mainWindow.FindTextBox();
                Assert.AreEqual(expected, textBox.Text);
            }
        }

        [TestCase(ScanCodeShort.KEY_A, false, "a")]
        public void PressScanCode(ScanCodeShort scanCode, bool isExtendedKey, string expected)
        {
            using (var app = Application.Launch(ExeFileName, "SingleTextBoxWindow"))
            {
                var mainWindow = app.MainWindow;
                Keyboard.PressScanCode((ushort)scanCode, isExtendedKey);
                Keyboard.ReleaseScanCode((ushort)scanCode, isExtendedKey);
                Wait.UntilInputIsProcessed();
                var textBox = mainWindow.FindTextBox();
                var sw = Stopwatch.StartNew();
                Assert.AreEqual(expected, textBox.Text);
            }
        }
    }
}
