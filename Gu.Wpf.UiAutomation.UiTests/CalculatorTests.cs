namespace Gu.Wpf.UiAutomation.UiTests
{
    using System;
    using System.Text.RegularExpressions;
    using NUnit.Framework;

    [TestFixture]
    public class CalculatorTests
    {
        private interface ICalculator
        {
            Button Button1 { get; }

            Button Button2 { get; }

            Button Button3 { get; }

            Button Button4 { get; }

            Button Button5 { get; }

            Button Button6 { get; }

            Button Button7 { get; }

            Button Button8 { get; }

            Button ButtonAdd { get; }

            Button ButtonEquals { get; }

            string Result { get; }
        }

        [Test]
        public void CalculatorTest()
        {
            if (WindowsVersion.IsAppVeyor())
            {
                return;
            }

            using (var app = StartApplication())
            {
                // Looks like it can take a long time on CI
                app.WaitForMainWindow(TimeSpan.FromSeconds(30));
                var window = app.MainWindow;
                var calc = WindowsVersion.IsWindows10()
                    ? (ICalculator)new Win10Calc(window)
                    : new LegacyCalc(window);
                Wait.For(TimeSpan.FromMilliseconds(200));

                // Switch to default mode
                Keyboard.TypeSimultaneously(Key.ALT, Key.KEY_1);
                window.WaitUntilResponsive();

                // Simple addition
                calc.Button1.Click();
                calc.Button2.Click();
                calc.Button3.Click();
                calc.Button4.Click();
                calc.ButtonAdd.Click();
                calc.Button5.Click();
                calc.Button6.Click();
                calc.Button7.Click();
                calc.Button8.Click();
                calc.ButtonEquals.Click();
                app.WaitWhileBusy();
                var result = calc.Result;
                Assert.AreEqual("6912", result);

                // Date comparison
                using (Keyboard.Hold(Key.CONTROL))
                {
                    Keyboard.Type(Key.KEY_E);
                }
            }
        }

        private static Application StartApplication()
        {
            if (WindowsVersion.IsWindows10())
            {
                // Use the store application on those systems
                return Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            }

            if (WindowsVersion.IsWindowsServer2016())
            {
                // The calc.exe on this system is just a stub which launches win32calc.exe
                return Application.Launch("win32calc.exe");
            }

            return Application.Launch("calc.exe");
        }

        public class Win10Calc : ICalculator
        {
            private readonly UiElement mainWindow;

            public Win10Calc(UiElement mainWindow)
            {
                this.mainWindow = mainWindow;
            }

            public Button Button1 => this.mainWindow.FindButton("num1Button");

            public Button Button2 => this.mainWindow.FindButton("num2Button");

            public Button Button3 => this.mainWindow.FindButton("num3Button");

            public Button Button4 => this.mainWindow.FindButton("num4Button");

            public Button Button5 => this.mainWindow.FindButton("num5Button");

            public Button Button6 => this.mainWindow.FindButton("num6Button");

            public Button Button7 => this.mainWindow.FindButton("num7Button");

            public Button Button8 => this.mainWindow.FindButton("num8Button");

            public Button ButtonAdd => this.mainWindow.FindButton("plusButton");

            public Button ButtonEquals => this.mainWindow.FindButton("equalButton");

            public string Result
            {
                get
                {
                    var resultElement = this.mainWindow.FindFirstDescendant(Conditions.ByAutomationId("CalculatorResults"));
                    var value = resultElement.Name;
                    return Regex.Replace(value, "[^0-9]", string.Empty);
                }
            }
        }

        private class LegacyCalc : ICalculator
        {
            private readonly UiElement mainWindow;

            internal LegacyCalc(UiElement mainWindow)
            {
                this.mainWindow = mainWindow;
            }

            public Button Button1 => this.mainWindow.FindButton("1");

            public Button Button2 => this.mainWindow.FindButton("2");

            public Button Button3 => this.mainWindow.FindButton("3");

            public Button Button4 => this.mainWindow.FindButton("4");

            public Button Button5 => this.mainWindow.FindButton("5");

            public Button Button6 => this.mainWindow.FindButton("6");

            public Button Button7 => this.mainWindow.FindButton("7");

            public Button Button8 => this.mainWindow.FindButton("8");

            public Button ButtonAdd => this.mainWindow.FindButton("Add");

            public Button ButtonEquals => this.mainWindow.FindButton("Equals");

            public string Result
            {
                get
                {
                    var resultElement = this.mainWindow.FindFirstDescendant(Conditions.ByAutomationId("158"));
                    var value = resultElement.Name;
                    return Regex.Replace(value, "[^0-9]", string.Empty);
                }
            }
        }
    }
}
