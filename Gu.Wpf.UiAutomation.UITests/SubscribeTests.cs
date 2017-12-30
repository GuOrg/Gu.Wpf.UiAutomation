namespace Gu.Wpf.UiAutomation.UiTests.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Automation;
    using NUnit.Framework;

    [TestFixture]
    public class SubscribeTests
    {
        private const string ExeFileName = "WpfApplication.exe";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Application.KillLaunched(ExeFileName);
        }

        [Test]
        public void FocusWindowFocusChanges()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "FocusWindow"))
            {
                var changes = new List<string>();
                var window = app.MainWindow;
                using (Subscribe.ToFocusChangedEvent(element => changes.Add(element.Name)))
                {
                    window.FindTextBox("TextBox2").Focus();
                    CollectionAssert.AreEqual(new[] { "TextBox2" }, changes);

                    window.FindTextBox("Button1").Focus();
                    CollectionAssert.AreEqual(new[] { "Button1" }, changes);
                }
            }
        }

        [Test]
        public void FocusWindowPropertyChanges()
        {
            using (var app = Application.AttachOrLaunch(ExeFileName, "FocusWindow"))
            {
                var changes = new List<string>();
                var window = app.MainWindow;
                var textBox = window.FindTextBox("TextBox1");

                using (Subscribe.ToPropertyChangedEvent(textBox.AutomationElement, TreeScope.Element, ValuePattern.ValueProperty, (e, p, o) => changes.Add($"{e.AutomationId}.{p.ProgrammaticName.Split('.')[1]} = {o}")))
                {
                    textBox.Text = "abc";
                    CollectionAssert.AreEqual(new[] { "TextBox1.ValueProperty = abc" }, changes);
                }
            }
        }

        [Test]
        public void FocusChangedWithPaintTest()
        {
            using (var app = Application.Launch("mspaint"))
            {
                var changes = new List<string>();
                var mainWindow = app.MainWindow;
                using (Subscribe.ToFocusChangedEvent(element => changes.Add(element.ToString())))
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
                        Assert.That(changes.Count, Is.GreaterThan(0));
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
