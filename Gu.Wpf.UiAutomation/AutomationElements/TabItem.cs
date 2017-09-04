namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Linq;

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
                var children = this.FindAllChildren();
                if (children.Count == 1 &&
                    children[0].ControlType == ControlType.Text)
                {
                    return children[0].Properties.Name.Value;
                }

                return this.Properties.Name.Value;
            }
        }

        public AutomationElement Header => this.FindAllChildren().FirstOrDefault();

        public AutomationElement Content
        {
            get
            {
                if (!this.IsSelected)
                {
                    throw new InvalidOperationException("TabItem must have be selected to get Content");
                }

                return this.FindAllChildren().ElementAtOrDefault(1);
            }
        }
    }
}
