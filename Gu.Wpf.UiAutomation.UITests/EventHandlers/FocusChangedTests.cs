namespace Gu.Wpf.UiAutomation.UITests.EventHandlers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    [TestFixture]
    public class FocusChangedTests
    {
        [Test]
        public void FocusChangedWithPaintTest()
        {
            using (var app = Application.Launch("mspaint"))
            {
                var focusChangedElements = new List<string>();
                var mainWindow = app.MainWindow();
                var x = app.Automation.RegisterFocusChangedEvent(element => { focusChangedElements.Add(element.ToString()); });
                Thread.Sleep(100);
                var button1 = mainWindow.FindButton(this.GetResizeText());
                button1.AsButton().Invoke();
                Thread.Sleep(100);
                var radio2 = mainWindow.FindRadioButton(this.GetPixelsText());
                Mouse.Click(MouseButton.Left, radio2.GetClickablePoint());
                Thread.Sleep(100);
                Keyboard.Press(VirtualKeyShort.ESCAPE);
                Thread.Sleep(100);
                app.Automation.UnRegisterFocusChangedEvent(x);
                mainWindow.Close();

                Assert.That(focusChangedElements.Count, Is.GreaterThan(0));
            }
        }

        private string GetResizeText()
        {
            switch (CultureInfo.InstalledUICulture.TwoLetterISOLanguageName)
            {
                case "de":
                    return "Größe ändern";
                default:
                    return "Resize";
            }
        }

        private string GetPixelsText()
        {
            switch (CultureInfo.InstalledUICulture.TwoLetterISOLanguageName)
            {
                case "de":
                    return "Pixel";
                default:
                    return "Pixels";
            }
        }
    }
}
