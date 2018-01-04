namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class OpenFileDialog : Dialog
    {
        public OpenFileDialog(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public TextBoxBase FileTextBox => this.FindTextBoxBase("File name:");

        public Button OpenButton => this.FindButton("Open");

        public void SetFileName(string fileName) => this.FileTextBox.ValuePattern.SetValue(fileName);
    }
}
