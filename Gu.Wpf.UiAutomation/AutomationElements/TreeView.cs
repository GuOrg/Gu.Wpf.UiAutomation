namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class TreeView : Selector<TreeViewItem>
    {
        public TreeView(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// The currently selected <see cref="TreeViewItem" />.
        /// </summary>
        public TreeViewItem? SelectedTreeViewItem => this.SearchSelectedItem(this.Items);

        private TreeViewItem? SearchSelectedItem(IReadOnlyList<TreeViewItem> treeItems)
        {
            // Search for a selected item in the direct children
            var directSelectedItem = treeItems.FirstOrDefault(t => t.IsSelected);
            if (directSelectedItem is { })
            {
                return directSelectedItem;
            }

            // Loop through the children and search in their children
            foreach (var treeItem in treeItems)
            {
                var selectedInChildItem = this.SearchSelectedItem(treeItem.Items);
                if (selectedInChildItem is { })
                {
                    return selectedInChildItem;
                }
            }

            return null;
        }
    }
}
