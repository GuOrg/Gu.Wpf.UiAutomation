namespace Gu.Wpf.UiAutomation
{
    /// <summary>
    /// An UI-item which supports the <see cref="ISelectionItemPattern" />
    /// </summary>
    public class SelectionItemAutomationElement : Control
    {
        public SelectionItemAutomationElement(BasicAutomationElementBase basicAutomationElement)
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
                    this.Select();
                }
            }
        }

        protected ISelectionItemPattern SelectionItemPattern => this.Patterns.SelectionItem.Pattern;

        /// <summary>
        /// Select this element.
        /// </summary>
        public SelectionItemAutomationElement Select()
        {
            this.ExecuteInPattern(this.SelectionItemPattern, throwIfNotSupported: true, action: pattern => pattern.Select());
            return this;
        }

        /// <summary>
        /// Add this element to the selection.
        /// </summary>
        public SelectionItemAutomationElement AddToSelection()
        {
            this.ExecuteInPattern(this.SelectionItemPattern, throwIfNotSupported: true, action: pattern => pattern.AddToSelection());
            return this;
        }

        /// <summary>
        /// Remove this element from the selection.
        /// </summary>
        public SelectionItemAutomationElement RemoveFromSelection()
        {
            this.ExecuteInPattern(this.SelectionItemPattern, throwIfNotSupported: true, action: pattern => pattern.RemoveFromSelection());
            return this;
        }
    }
}
