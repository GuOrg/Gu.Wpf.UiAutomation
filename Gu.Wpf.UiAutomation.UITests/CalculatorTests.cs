namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Text.RegularExpressions;
    using Gu.Wpf.UiAutomation.AutomationElements;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.Input;
    using Gu.Wpf.UiAutomation.Tools;
    using Gu.Wpf.UiAutomation.UITests.TestFramework;
    using Gu.Wpf.UiAutomation.WindowsAPI;
    using NUnit.Framework;

    [TestFixture]
    public class CalculatorTests : UITestBase
    {
        public CalculatorTests()
            : base(AutomationType.UIA3, TestApplicationType.Custom)
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

    public interface ICalculator
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

    public class LegacyCalc : ICalculator
    {
        private readonly AutomationElement mainWindow;

        public Button Button1 => this.FindElement("1").AsButton();

        public Button Button2 => this.FindElement("2").AsButton();

        public Button Button3 => this.FindElement("3").AsButton();

        public Button Button4 => this.FindElement("4").AsButton();

        public Button Button5 => this.FindElement("5").AsButton();

        public Button Button6 => this.FindElement("6").AsButton();

        public Button Button7 => this.FindElement("7").AsButton();

        public Button Button8 => this.FindElement("8").AsButton();

        public Button ButtonAdd => this.FindElement("Add").AsButton();

        public Button ButtonEquals => this.FindElement("Equals").AsButton();

        public string Result
        {
            get
            {
                var resultElement = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("158"));
                var value = resultElement.Properties.Name;
                return Regex.Replace(value, "[^0-9]", string.Empty);
            }
        }

        public LegacyCalc(AutomationElement mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private AutomationElement FindElement(string text)
        {
            var element = this.mainWindow.FindFirstDescendant(cf => cf.ByText(text));
            return element;
        }
    }

    public class Win10Calc : ICalculator
    {
        private readonly AutomationElement mainWindow;

        public Button Button1 => this.FindElement("num1Button").AsButton();

        public Button Button2 => this.FindElement("num2Button").AsButton();

        public Button Button3 => this.FindElement("num3Button").AsButton();

        public Button Button4 => this.FindElement("num4Button").AsButton();

        public Button Button5 => this.FindElement("num5Button").AsButton();

        public Button Button6 => this.FindElement("num6Button").AsButton();

        public Button Button7 => this.FindElement("num7Button").AsButton();

        public Button Button8 => this.FindElement("num8Button").AsButton();

        public Button ButtonAdd => this.FindElement("plusButton").AsButton();

        public Button ButtonEquals => this.FindElement("equalButton").AsButton();

        public string Result
        {
            get
            {
                var resultElement = this.FindElement("CalculatorResults");
                var value = resultElement.Properties.Name;
                return Regex.Replace(value, "[^0-9]", string.Empty);
            }
        }

        public Win10Calc(AutomationElement mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private AutomationElement FindElement(string text)
        {
            var element = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(text));
            return element;
        }
    }
}
