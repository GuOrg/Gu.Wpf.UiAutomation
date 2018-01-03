namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ListBoxItem : SelectionItemControl
    {
        public ListBoxItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ListBox ContainingListBox => (ListBox)FromAutomationElement(this.SelectionItemPattern.Current.SelectionContainer);

        public string Text => this.AutomationElement.Text();

        public ScrollItemPattern ScrollItemPattern => this.AutomationElement.ScrollItemPattern();

        public static SelectionItemControl Create(AutomationElement automationElement)
        {
            return Conditions.IsMatch(automationElement.Parent(), Conditions.ComboBox)
                ? (SelectionItemControl)new ComboBoxItem(automationElement)
                : new ListBoxItem(automationElement);
        }

        public ListBoxItem ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}
