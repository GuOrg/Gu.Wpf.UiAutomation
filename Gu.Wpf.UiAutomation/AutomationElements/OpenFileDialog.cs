namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class OpenFileDialog : Dialog
    {
        private static readonly Condition OpenButtonCondition = WindowsVersion.IsWindows10()
            ? new AndCondition(
                Conditions.ByClassName("Button"),
                Conditions.ByNameOrAutomationId("Open"))
            : new AndCondition(
                Conditions.Button,
                Conditions.ByNameOrAutomationId("Open"));

        public OpenFileDialog(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TextBoxBase FileTextBox => this.FindTextBoxBase("File name:");

        public Button OpenButton => this.FindFirst(
            TreeScope.Descendants,
            OpenButtonCondition,
            x => new Button(x),
            Retry.Time);

        public void SetFileName(string fileName) => this.FileTextBox.ValuePattern.SetValue(fileName);
    }
}
