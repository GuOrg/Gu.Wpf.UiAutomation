namespace Gu.Wpf.UiAutomation.UITests
{
    using System;
    using NUnit.Framework;
    using OperatingSystem = Gu.Wpf.UiAutomation.OperatingSystem;

    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void CalculatorTest()
        {
            using (var app = StartApplication())
            {
                var window = app.MainWindow;
                var calc = Gu.Wpf.UiAutomation.OperatingSystem.IsWindows10()
                    ? (ICalculator)new Win10Calc(window)
                    : new LegacyCalc(window);
                if (OperatingSystem.IsWindows7())
                {
                    Wait.For(TimeSpan.FromMilliseconds(200));
                }

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
                using (Keyboard.Pressing(Key.CONTROL))
                {
                    Keyboard.Type(Key.KEY_E);
                }
            }
        }

        private static Application StartApplication()
        {
            if (Gu.Wpf.UiAutomation.OperatingSystem.IsWindows10())
            {
                // Use the store application on those systems
                return Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            }

            if (Gu.Wpf.UiAutomation.OperatingSystem.IsWindowsServer2016())
            {
                // The calc.exe on this system is just a stub which launches win32calc.exe
                return Application.Launch("win32calc.exe");
            }

            return Application.Launch("calc.exe");
        }
    }
}
