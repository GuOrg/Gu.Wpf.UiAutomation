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

        private AutomationElement FindElement(string text)
        {
            var element = this.mainWindow.FindFirstDescendant(cf => cf.ByText(text));
            return element;
        }
    }
}