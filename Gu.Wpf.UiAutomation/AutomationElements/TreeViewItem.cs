namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;

    public class TreeViewItem : Control
    {
        private readonly SelectionItemAutomationElement selectionItemAutomationElement;
        private readonly ExpandCollapseAutomationElement expandCollapseAutomationElement;
        private readonly ConditionBase treeViewItemCondition;

        public TreeViewItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
            this.selectionItemAutomationElement = new SelectionItemAutomationElement(basicAutomationElement);
            this.expandCollapseAutomationElement = new ExpandCollapseAutomationElement(basicAutomationElement);
            this.treeViewItemCondition = this.ConditionFactory.ByControlType(ControlType.TreeItem);
        }

        /// <summary>
        /// All child <see cref="TreeViewItem" /> objects from this <see cref="TreeViewItem" />
        /// </summary>
        public IReadOnlyList<TreeViewItem> Items => this.BasicAutomationElement.FindAll(
            TreeScope.Children,
            this.treeViewItemCondition,
            x => new TreeViewItem(x));

        /// <summary>
        /// The text of the <see cref="TreeViewItem" />
        /// </summary>
        public string Text
        {
            get
            {
                var value = this.Properties.Name.Value;
                if (string.IsNullOrEmpty(value) || value.Contains("System.Windows.Controls.TreeViewItem"))
                {
                    var textElement = this.FindFirstChild(cf => cf.ByControlType(ControlType.Text));
                    return textElement == null ? string.Empty : textElement.Properties.Name.Value;
                }

                return value;
            }
        }

        public bool IsSelected
        {
            get => this.selectionItemAutomationElement.IsSelected;
            set => this.selectionItemAutomationElement.IsSelected = value;
        }

        public bool IsExpanded
        {
            get
            {
                if (this.Patterns.ExpandCollapse.TryGetPattern(out var pattern) &&
                    pattern.ExpandCollapseState.TryGetValue(out var state))
                {
                    return state == ExpandCollapseState.Expanded;
                }

                return true;
            }

            set
            {
                if (value)
                {
                    this.Expand();
                }
                else
                {
                    this.Collapse();
                }
            }
        }

        public void Expand()
        {
            this.Patterns.ExpandCollapse.Pattern.Expand();
        }

        public void Collapse()
        {
            this.Patterns.ExpandCollapse.Pattern.Collapse();
        }

        public void Select()
        {
            this.Patterns.SelectionItem.Pattern.Select();
        }
    }
}
