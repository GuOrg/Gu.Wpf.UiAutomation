namespace Gu.Wpf.UiAutomation.UITests
{
    using System.Text.RegularExpressions;

    public class LegacyCalc : ICalculator
    {
        private readonly AutomationElement mainWindow;

        public LegacyCalc(AutomationElement mainWindow)
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
                var resultElement = this.mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("158"));
                var value = resultElement.Properties.Name;
                return Regex.Replace(value.Value, "[^0-9]", string.Empty);
            }
        }
    }
}