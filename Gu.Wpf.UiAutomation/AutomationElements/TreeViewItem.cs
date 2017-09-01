namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;

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
        public IReadOnlyList<TreeViewItem> Items => this.BasicAutomationElement.FindAll(TreeScope.Children, this.treeViewItemCondition)
                                                        .Select(e => e.AsTreeViewItem())
                                                        .ToArray();

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
                    return textElement == null ? string.Empty : textElement.Properties.Name;
                }

                return value;
            }
        }

        public bool IsSelected
        {
            get => this.selectionItemAutomationElement.IsSelected;
            set => this.selectionItemAutomationElement.IsSelected = value;
        }

        public void Expand()
        {
            this.expandCollapseAutomationElement.Expand();
        }

        public void Collapse()
        {
            this.expandCollapseAutomationElement.Collapse();
        }

        public void Select()
        {
            this.selectionItemAutomationElement.Select();
        }
    }
}
