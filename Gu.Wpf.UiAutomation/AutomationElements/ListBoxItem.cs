namespace Gu.Wpf.UiAutomation
{
    public class ListBoxItem : SelectionItemAutomationElement
    {
        public ListBoxItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        protected IScrollItemPattern ScrollItemPattern => this.Patterns.ScrollItem.Pattern;

        public ListBoxItem ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}