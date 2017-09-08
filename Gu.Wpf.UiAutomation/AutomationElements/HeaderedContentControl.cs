namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HeaderedContentControl : Control
    {
        public HeaderedContentControl(BasicAutomationElementBase basicAutomationElement)
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

        /// <summary>
        /// The first child so it assumes there is exactly one element in the header.
        /// </summary>
        public AutomationElement Header => this.FindFirstChild();

        /// <summary>
        /// When the content is a single item.
        /// This returns this.FindAllChildren().Skip(1).Single();
        /// So it assumes there is exactly one element in the header.
        /// </summary>
        public AutomationElement Content
        {
            get
            {
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
        public IReadOnlyList<AutomationElement> ContentCollection => this.FindAllChildren().Skip(1).ToArray();
    }
}