namespace Gu.Wpf.UiAutomation
{
    public class ListBoxItem : SelectionItemAutomationElement
    {
        public ListBoxItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public virtual string Text
        {
            get
            {
                if (this.FrameworkType == FrameworkType.Wpf)
                {
                    // In WPF, the Text is actually an inner content only (text) element
                    // which can be accessed only with a raw walker.
                    var rawTreeWalker = this.Automation.TreeWalkerFactory.GetRawViewWalker();
                    var rawElement = rawTreeWalker.GetFirstChild(this);
                    if (rawElement != null)
                    {
                        return rawElement.Properties.Name.Value;
                    }
                }

                return this.BasicAutomationElement.Properties.Name.Value;
            }
        }

        protected IScrollItemPattern ScrollItemPattern => this.Patterns.ScrollItem.Pattern;

        public ListBoxItem ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }
}