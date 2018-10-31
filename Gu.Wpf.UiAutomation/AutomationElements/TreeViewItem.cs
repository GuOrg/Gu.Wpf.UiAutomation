namespace Gu.Wpf.UiAutomation
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class TreeViewItem : SelectionItemControl
    {
        public TreeViewItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// All child <see cref="TreeViewItem" /> objects from this <see cref="TreeViewItem" />.
        /// </summary>
        public IReadOnlyList<TreeViewItem> Items => this.AutomationElement.FindAll(
            TreeScope.Children,
            Conditions.TreeViewItem,
            x => new TreeViewItem(x));

        /// <summary>
        /// The text of the <see cref="TreeViewItem" />.
        /// </summary>
        public string Text
        {
            get
            {
                var text = this.Name;
                if (string.IsNullOrEmpty(text) ||
                    text.Contains("System.Windows.Controls.TreeViewItem"))
                {
                    return this.AutomationElement.Text();
                }

                return text;
            }
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
    }
}
