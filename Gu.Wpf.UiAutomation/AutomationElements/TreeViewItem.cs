namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;

    public class TreeViewItem : Control
    {
        private readonly SelectionItemAutomationElement selectionItemAutomationElement;
        private readonly ExpandCollapseAutomationElement expandCollapseAutomationElement;

        public TreeViewItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
            this.selectionItemAutomationElement = new SelectionItemAutomationElement(basicAutomationElement);
            this.expandCollapseAutomationElement = new ExpandCollapseAutomationElement(basicAutomationElement);
        }

        /// <summary>
        /// All child <see cref="TreeViewItem" /> objects from this <see cref="TreeViewItem" />
        /// </summary>
        public IReadOnlyList<TreeViewItem> Items => this.GetTreeItems();

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

        /// <summary>
        /// Gets all the <see cref="TreeViewItem" /> objects for this <see cref="TreeViewItem" />
        /// </summary>
        private IReadOnlyList<TreeViewItem> GetTreeItems()
        {
            return this.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem))
                       .Select(e => e.AsTreeViewItem())
                       .ToArray();
        }
    }
}
