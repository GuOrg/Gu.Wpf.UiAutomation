namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class TabItem : SelectionItemAutomationElement
    {
        public TabItem(AutomationElement automationElement)
            : base(automationElement)
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

        public UiElement Header => this.FindFirstChild();

        public UiElement Content
        {
            get
            {
                if (!this.IsSelected)
                {
                    throw new InvalidOperationException("TabItem must have be selected to get Content");
                }

                var children = this.FindAllChildren();
                if (children.Count < 2)
                {
                    throw new InvalidOperationException($"{this.GetType().Name} does not have content.");
                }

                if (children.Count > 2)
                {
                    throw new InvalidOperationException($"{this.GetType().Name} has an itemscontrol as content. Use ContentCollection");
                }

                return children[1];
            }
        }

        /// <summary>
        /// When the content is an itemscontrol.
        /// </summary>
        public IReadOnlyList<UiElement> ContentCollection => this.FindAllChildren().Skip(1).ToArray();
    }
}
