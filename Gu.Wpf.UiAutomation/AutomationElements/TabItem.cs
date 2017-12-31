namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Windows.Automation;

    public class TabItem : HeaderedContentControl
    {
        public TabItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Flag to get/set the selection of this element.
        /// </summary>
        public bool IsSelected => this.SelectionItemPattern.Current.IsSelected;

        public override UiElement Content
        {
            get
            {
                if (!this.IsSelected)
                {
                    throw new InvalidOperationException("TabItem must have be selected to get Content.");
                }

                return base.Content;
            }
        }

        protected SelectionItemPattern SelectionItemPattern => this.AutomationElement.SelectionItemPattern();

        /// <summary>
        /// Select this element.
        /// </summary>
        public TabItem Select()
        {
            this.SelectionItemPattern.Select();
            return this;
        }
    }
}
