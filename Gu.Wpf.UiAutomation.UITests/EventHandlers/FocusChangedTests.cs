namespace Gu.Wpf.UiAutomation.UiTests.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
                var mainWindow = app.MainWindow;
                using (Subscribe.ToFocusChangedEvent(element => { focusChangedElements.Add(element.ToString()); }))
                {
                    Wait.For(TimeSpan.FromMilliseconds(100));
                    var button1 = mainWindow.FindButton(this.GetResizeText());
                    button1.Invoke();
                    Wait.For(TimeSpan.FromMilliseconds(100));
                    var radio2 = mainWindow.FindRadioButton(this.GetPixelsText());
                    Mouse.Click(MouseButton.Left, radio2.GetClickablePoint());
                    Wait.For(TimeSpan.FromMilliseconds(100));
                    using (Keyboard.Pressing(Key.ESCAPE))
                    {
                        Wait.For(TimeSpan.FromMilliseconds(100));
                        mainWindow.Close();
                        Assert.That(focusChangedElements.Count, Is.GreaterThan(0));
                    }
                }
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
