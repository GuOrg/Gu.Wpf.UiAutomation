namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class SaveFileDialog : Dialog
    {
        public SaveFileDialog(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TextBoxBase FileTextBox => this.FindTextBoxBase("File name:");

        public Button SaveButton => this.FindButton("Save");

        public void SetFileName(string fileName) => this.FileTextBox.ValuePattern.SetValue(fileName);
    }
}
