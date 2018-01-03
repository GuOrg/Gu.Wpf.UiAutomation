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

        public virtual string Text
        {
            get
            {
                if (this.FrameworkType == FrameworkType.Wpf)
                {
                    // In WPF, the Text is actually an inner content only (text) element
                    // which can be accessed only with a raw walker.
                    var rawElement = TreeWalker.RawViewWalker.GetFirstChild(this.AutomationElement);
                    if (rawElement != null)
                    {
                        return rawElement.Name();
                    }
                }

                return this.AutomationElement.Name();
            }
        }

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
