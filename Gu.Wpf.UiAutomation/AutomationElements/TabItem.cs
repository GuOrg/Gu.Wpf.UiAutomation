namespace Gu.Wpf.UiAutomation
{
    using System;

    public class TabItem : SelectionItemAutomationElement
    {
        public TabItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// The header text.
        /// </summary>
        public string Text
        {
            get
            {
                var header = this.Header;
                if (header != null)
                {
                    return header.Properties.Name.Value;
                }

                return this.Properties.Name.Value;
            }
        }

        public AutomationElement Header => this.FindFirstChild();

        public AutomationElement Content
        {
            get
            {
                if (!this.IsSelected)
                {
                    throw new InvalidOperationException("TabItem must have be selected to get Content");
                }

                return this.FindChildAt(1);
            }
        }
    }
}
