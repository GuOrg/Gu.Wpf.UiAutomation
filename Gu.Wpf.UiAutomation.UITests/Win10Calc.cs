namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Text.RegularExpressions;

    public class Win10Calc : ICalculator
    {
        private readonly AutomationElement mainWindow;

        public Win10Calc(AutomationElement mainWindow)
        {
            this.mainWindow = mainWindow;
        }

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

        private AutomationElement FindElement(string text)
        {
            var element = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(text));
            return element;
        }
    }
}