namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;

    public class Tree : AutomationElement
    {
        public Tree(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// The currently selected <see cref="TreeItem" />
        /// </summary>
        public TreeItem SelectedTreeItem => this.SearchSelectedItem(this.TreeItems);

        /// <summary>
        /// All child <see cref="TreeItem" /> objects from this <see cref="Tree" />
        /// </summary>
        public IReadOnlyList<TreeItem> TreeItems => this.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem))
                                                          .Select(e => e.AsTreeItem())
                                                          .ToArray();

        private TreeItem SearchSelectedItem(IReadOnlyList<TreeItem> treeItems)
        {
            // Search for a selected item in the direct children
            var directSelectedItem = treeItems.FirstOrDefault(t => t.IsSelected);
            if (directSelectedItem != null)
            {
                return directSelectedItem;
            }

            // Loop thru the children and search in their children
            foreach (var treeItem in treeItems)
            {
                var selectedInChildItem = this.SearchSelectedItem(treeItem.TreeItems);
                if (selectedInChildItem != null)
                {
                    return selectedInChildItem;
                }
            }

            return null;
        }
    }
}
