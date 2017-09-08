namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ContentControl : Control
    {
        public ContentControl(BasicAutomationElementBase basicAutomationElement)
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
                var header = this.Content;
                if (header != null)
                {
                    return header.Properties.Name.Value;
                }

                return this.Properties.Name.Value;
            }
        }

        public AutomationElement Content
        {
            get
            {
                var children = this.FindAllChildren();
                if (children.Count < 1)
                {
                    throw new InvalidOperationException($"{this.GetType().Name} does not have content.");
                }

                if (children.Count > 1)
                {
                    throw new InvalidOperationException($"{this.GetType().Name} has an itemscontrol as content. Use ContentCollection");
                }

                return children[0];
            }
        }

        /// <summary>
        /// When the content is an itemscontrol.
        /// </summary>
        public IReadOnlyList<AutomationElement> ContentCollection => this.FindAllChildren().Skip(1).ToArray();
    }
}