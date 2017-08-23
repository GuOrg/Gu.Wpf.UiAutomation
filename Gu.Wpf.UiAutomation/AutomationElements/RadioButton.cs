namespace Gu.Wpf.UiAutomation
{
    public class RadioButton : Control
    {
        public RadioButton(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Flag to get/set the selection of this element.
        /// </summary>
        public bool IsChecked
        {
            get => this.SelectionItemPattern.IsSelected;
            set
            {
                if (this.IsChecked == value)
                {
                    return;
                }

                if (value && !this.IsChecked)
                {
                    this.ExecuteInPattern(this.SelectionItemPattern, throwIfNotSupported: true, action: pattern => pattern.Select());
                }

                if (this.IsChecked != value)
                {
                    throw new UiAutomationException($"Setting {this} .IsChecked to {value}");
                }
            }
        }

        protected ISelectionItemPattern SelectionItemPattern => this.Patterns.SelectionItem.Pattern;

        public void Toggle()
        {
            this.IsChecked = !this.IsChecked;
        }
    }
}
