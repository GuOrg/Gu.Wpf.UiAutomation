namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class ListViewItem : GridRow
    {
        public ListViewItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public ListView ContainingListView => (ListView)FromAutomationElement(this.SelectionItemPattern.Current.SelectionContainer);
    }
}
