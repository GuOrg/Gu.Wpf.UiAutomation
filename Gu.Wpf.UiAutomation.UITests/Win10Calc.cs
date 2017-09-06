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
                var resultElement = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("CalculatorResults"));
                var value = resultElement.Properties.Name;
                return Regex.Replace(value.Value, "[^0-9]", string.Empty);
            }
        }
    }
}