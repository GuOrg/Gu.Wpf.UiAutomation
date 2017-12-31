namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    /// <summary>
    /// Represents an item in a <see cref="ComboBox"/>
    /// </summary>
    public class ComboBoxItem : SelectionItemAutomationElement
    {
        public ComboBoxItem(AutomationElement automationElement)
            : base(automationElement)
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
                    var rawElement = TreeWalker.RawViewWalker.GetFirstChild(this.AutomationElement);
                    if (rawElement != null)
                    {
                        return rawElement.Name();
                    }
                }

                return this.AutomationElement.Name();
            }
        }
    }
}
