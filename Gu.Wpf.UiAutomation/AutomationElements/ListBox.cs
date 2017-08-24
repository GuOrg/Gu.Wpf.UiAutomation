namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ListBox : Control
    {
        public ListBox(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Gets the total row count.
        /// </summary>
        public int RowCount => this.GridPattern.RowCount.Value;

        /// <summary>
        /// Returns the rows which are currently visible to UIA. Might not be the full list (eg. in virtualized lists)!
        /// Use <see cref="GetItemByIndex" /> to make sure to get the correct row.
        /// </summary>
        public virtual IReadOnlyList<ListBoxItem> Items
        {
            get
            {
                return this.FindAllChildren().Select(x => x.AsListBoxItem()).ToArray();
            }
        }

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public IReadOnlyList<ListBoxItem> SelectedItems => this.SelectionPattern.Selection.Value.Select(x => new ListBoxItem(x.BasicAutomationElement)).ToArray();

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public ListBoxItem SelectedItem => this.SelectedItems?.FirstOrDefault();

        protected IGridPattern GridPattern => this.Patterns.Grid.Pattern;

        protected ITablePattern TablePattern => this.Patterns.Table.Pattern;

        protected ISelectionPattern SelectionPattern => this.Patterns.Selection.Pattern;

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public ListBoxItem Select(int rowIndex)
        {
            var item = this.GetItemByIndex(rowIndex);
            item.Select();
            return item;
        }

        /// <summary>
        /// Add a row to the selection by index.
        /// </summary>
        public ListBoxItem AddToSelection(int rowIndex)
        {
            var item = this.GetItemByIndex(rowIndex);
            item.AddToSelection();
            return item;
        }

        /// <summary>
        /// Remove a row from the selection by index.
        /// </summary>
        public ListBoxItem RemoveFromSelection(int rowIndex)
        {
            var item = this.GetItemByIndex(rowIndex);
            item.RemoveFromSelection();
            return item;
        }

        /// <summary>
        /// Get a row by index.
        /// </summary>
        public ListBoxItem GetItemByIndex(int rowIndex)
        {
            this.PreCheckRow(rowIndex);
            var item = this.GridPattern.GetItem(rowIndex, 0).AsListBoxItem();
            return item;
        }

        private void PreCheckRow(int rowIndex)
        {
            if (this.RowCount <= rowIndex)
            {
                throw new Exception($"Grid contains only {this.RowCount} row(s) but index {rowIndex} was requested");
            }
        }
    }
}