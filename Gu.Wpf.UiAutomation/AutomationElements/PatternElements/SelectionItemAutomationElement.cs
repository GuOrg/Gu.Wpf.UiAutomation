namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    /// <summary>
    /// An UI-item which supports the <see cref="ISelectionItemPattern" />
    /// </summary>
    public class SelectionItemAutomationElement : Control
    {
        public SelectionItemAutomationElement(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Flag to get/set the selection of this element.
        /// </summary>
        public bool IsSelected
        {
            get => this.SelectionItemPattern.IsSelected.ValueOrDefault();
            set
            {
                if (this.IsSelected == value)
                {
                    return;
                }

                if (value && !this.IsSelected)
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
