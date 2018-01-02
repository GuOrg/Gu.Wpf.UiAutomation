namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class TabItem : SelectionItemControl
    {
        public TabItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// The header text.
        /// </summary>
        public string HeaderText
        {
            get
            {
                var header = this.Header;
                if (header != null)
                {
                    return header.Name;
                }

                return this.Name;
            }
        }

        /// <summary>
        /// The first child so it assumes there is exactly one element in the header.
        /// </summary>
        public UiElement Header => this.FindFirstChild();

        /// <summary>
        /// When the content is a single item.
        /// This returns this.FindAllChildren().Skip(1).Single();
        /// So it assumes there is exactly one element in the header.
        /// </summary>
        public virtual UiElement Content
        {
            get
            {
                if (!this.IsSelected)
                {
                    throw new InvalidOperationException("TabItem must have be selected to get Content.");
                }

                var children = this.FindAllChildren();
                if (children.Count < 2)
                {
                    throw new InvalidOperationException($"{this.GetType().Name} does not have content.");
                }

                if (children.Count > 2)
                {
                    throw new InvalidOperationException($"{this.GetType().Name} has an ItemsControl as content. Use ContentCollection");
                }

                return children[1];
            }
        }

        /// <summary>
        /// When the content is an ItemsControl.
        /// This returns this.FindAllChildren().Skip(1).ToArray();
        /// So it assumes there is exactly one element in the header.
        /// </summary>
        public IReadOnlyList<UiElement> ContentCollection => this.FindAllChildren().Skip(1).ToArray();

        /// <summary>
        /// When the content is an ItemsControl.
        /// This returns this.FindAllChildren().Skip(1).ToArray();
        /// So it assumes there is exactly one element in the header.
        /// </summary>
        public IReadOnlyList<T> ContentElements<T>(Func<AutomationElement, T> wrap)
            where T : UiElement
        {
            return this.FindAllChildren(System.Windows.Automation.Condition.TrueCondition, wrap).Skip(1).ToArray();
        }
    }
}
