namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class TreeViewItem : Control
    {
        private readonly SelectionItemAutomationElement selectionItemAutomationElement;
        private readonly ExpandCollapseAutomationElement expandCollapseAutomationElement;
        private readonly ConditionBase treeViewItemCondition;

        public TreeViewItem(AutomationElement automationElement)
            : base(automationElement)
        {
            this.selectionItemAutomationElement = new SelectionItemAutomationElement(automationElement);
            this.expandCollapseAutomationElement = new ExpandCollapseAutomationElement(automationElement);
            this.treeViewItemCondition = this.ConditionFactory.ByControlType(ControlType.TreeItem);
        }

        /// <summary>
        /// All child <see cref="TreeViewItem" /> objects from this <see cref="TreeViewItem" />
        /// </summary>
        public IReadOnlyList<TreeViewItem> Items => this.AutomationElement.FindAll(
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
                var value = this.Name;
                if (string.IsNullOrEmpty(value) || value.Contains("System.Windows.Controls.TreeViewItem"))
                {
                    var textElement = this.FindFirstChild(cf => cf.ByControlType(ControlType.Text));
                    return textElement == null ? string.Empty : textElement.Name;
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
                if (this.AutomationElement.TryGetExpandCollapsePattern(out var pattern))
                {
                    return pattern.Current.ExpandCollapseState == ExpandCollapseState.Expanded;
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
            this.AutomationElement.ExpandCollapsePattern().Expand();
        }

        public void Collapse()
        {
            this.AutomationElement.ExpandCollapsePattern().Collapse();
        }

        public void Select()
        {
            this.AutomationElement.SelectionItemPattern().Select();
        }
    }
}
