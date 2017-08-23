namespace Gu.Wpf.UiAutomation
{
    using System.Linq;

    public class TreeItem : AutomationElement
    {
        private readonly SelectionItemAutomationElement selectionItemAutomationElement;
        private readonly ExpandCollapseAutomationElement expandCollapseAutomationElement;

        public TreeItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
            this.selectionItemAutomationElement = new SelectionItemAutomationElement(basicAutomationElement);
            this.expandCollapseAutomationElement = new ExpandCollapseAutomationElement(basicAutomationElement);
        }

        /// <summary>
        /// All child <see cref="TreeItem" /> objects from this <see cref="TreeItem" />
        /// </summary>
        public TreeItem[] TreeItems => this.GetTreeItems();

        /// <summary>
        /// The text of the <see cref="TreeItem" />
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
            get => this.selectionItemAutomationElement.IsChecked;
            set => this.selectionItemAutomationElement.IsChecked = value;
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
        /// Gets all the <see cref="TreeItem" /> objects for this <see cref="TreeItem" />
        /// </summary>
        private TreeItem[] GetTreeItems()
        {
            return this.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem))
                .Select(e => e.AsTreeItem()).ToArray();
        }
    }
}
