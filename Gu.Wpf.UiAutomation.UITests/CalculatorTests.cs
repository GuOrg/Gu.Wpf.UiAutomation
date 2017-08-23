namespace Gu.Wpf.UiAutomation.UITests
{
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    [TestFixture]
    public class CalculatorTests : UITestBase
    {
        public CalculatorTests()
            : base(TestApplicationType.Custom)
        {
        }

        [Test]
        public void CalculatorTest()
        {
            var window = this.App.GetMainWindow(this.Automation);
            var calc = SystemProductNameFetcher.IsWindows10() ? (ICalculator)new Win10Calc(window) : new LegacyCalc(window);

            // Switch to default mode
            System.Threading.Thread.Sleep(1000);
            Keyboard.TypeSimultaneously(VirtualKeyShort.ALT, VirtualKeyShort.KEY_1);
            Helpers.WaitUntilInputIsProcessed();
            this.App.WaitWhileBusy();
            System.Threading.Thread.Sleep(1000);

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
            this.App.WaitWhileBusy();
            var result = calc.Result;
            Assert.That(result, Is.EqualTo("6912"));

            // Date comparison
            using (Keyboard.Pressing(VirtualKeyShort.CONTROL))
            {
                Keyboard.Type(VirtualKeyShort.KEY_E);
            }
        }

        protected override Application StartApplication()
        {
            if (SystemProductNameFetcher.IsWindows10())
            {
                // Use the store application on those systems
                return Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            }

            if (SystemProductNameFetcher.IsWindowsServer2016())
            {
                // The calc.exe on this system is just a stub which launches win32calc.exe
                return Application.Launch("win32calc.exe");
            }

            return Application.Launch("calc.exe");
        }
    }
}
