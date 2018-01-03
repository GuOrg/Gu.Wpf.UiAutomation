namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    /// <summary>
    /// Represents an item in a <see cref="ComboBox"/>
    /// </summary>
    public class ComboBoxItem : SelectionItemControl
    {
        public ComboBoxItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ComboBox ContainingListBox => (ComboBox)FromAutomationElement(this.SelectionItemPattern.Current.SelectionContainer);

        public string Text => this.AutomationElement.Text();
    }
}
