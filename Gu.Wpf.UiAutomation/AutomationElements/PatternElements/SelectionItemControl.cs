namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    /// <summary>
    /// An UI-item which supports the <see cref="SelectionItemPattern" />
    /// </summary>
    public class SelectionItemControl : Control
    {
        public SelectionItemControl(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Flag to get/set the selection of this element.
        /// </summary>
        public bool IsSelected
        {
            get => this.SelectionItemPattern.Current.IsSelected;
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

        public SelectionItemPattern SelectionItemPattern => this.AutomationElement.SelectionItemPattern();

        /// <summary>
        /// Select this element.
        /// </summary>
        public SelectionItemControl Select()
        {
            this.SelectionItemPattern.Select();
            return this;
        }

        /// <summary>
        /// Add this element to the selection.
        /// </summary>
        public SelectionItemControl AddToSelection()
        {
            this.SelectionItemPattern.AddToSelection();
            return this;
        }

        /// <summary>
        /// Remove this element from the selection.
        /// </summary>
        public SelectionItemControl RemoveFromSelection()
        {
            this.SelectionItemPattern.RemoveFromSelection();
            return this;
        }
    }
}
