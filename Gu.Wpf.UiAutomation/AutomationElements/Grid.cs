namespace Gu.Wpf.UiAutomation.AutomationElements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gu.Wpf.UiAutomation.AutomationElements.Infrastructure;
    using Gu.Wpf.UiAutomation.AutomationElements.PatternElements;
    using Gu.Wpf.UiAutomation.Definitions;
    using Gu.Wpf.UiAutomation.Patterns;

    /// <summary>
    /// Element for grids and tables.
    /// </summary>
    public class Grid : AutomationElement
    {
        public Grid(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Gets the total row count.
        /// </summary>
        public int RowCount => this.GridPattern.RowCount.Value;

        /// <summary>
        /// Gets the total column count.
        /// </summary>
        public int ColumnCount => this.GridPattern.ColumnCount.Value;

        /// <summary>
        /// Gets all column header elements.
        /// </summary>
        public AutomationElement[] ColumnHeaders => this.TablePattern.ColumnHeaders.Value;

        /// <summary>
        /// Gets all row header elements.
        /// </summary>
        public AutomationElement[] RowHeaders => this.TablePattern.RowHeaders.Value;

        /// <summary>
        /// Gets whether the data should be read primarily by row or by column.
        /// </summary>
        public RowOrColumnMajor RowOrColumnMajor => this.TablePattern.RowOrColumnMajor.Value;

        /// <summary>
        /// Gets the header item.
        /// </summary>
        public virtual GridHeader Header
        {
            get
            {
                var header = this.FindFirstChild(cf => cf.ByControlType(ControlType.Header));
                return header?.AsGridHeader();
            }
        }

        /// <summary>
        /// Returns the rows which are currently visible to UIA. Might not be the full list (eg. in virtualized lists)!
        /// Use <see cref="GetRowByIndex" /> to make sure to get the correct row.
        /// </summary>
        public virtual GridRow[] Rows
        {
            get
            {
                var rows = this.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem).Or(cf.ByControlType(ControlType.ListItem)));
                return rows.Select(x => x.AsGridRow()).ToArray();
            }
        }

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public GridRow[] SelectedItems => this.SelectionPattern.Selection.Value.Select(x => new GridRow(x.BasicAutomationElement)).ToArray();

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public GridRow SelectedItem => this.SelectedItems?.FirstOrDefault();

        protected IGridPattern GridPattern => this.Patterns.Grid.Pattern;

        protected ITablePattern TablePattern => this.Patterns.Table.Pattern;

        protected ISelectionPattern SelectionPattern => this.Patterns.Selection.Pattern;

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public GridRow Select(int rowIndex)
        {
            var gridRow = this.GetRowByIndex(rowIndex);
            gridRow.Select();
            return gridRow;
        }

        /// <summary>
        /// Select the first row by text in the given column.
        /// </summary>
        public GridRow Select(int columnIndex, string textToFind)
        {
            var gridRow = this.GetRowByValue(columnIndex, textToFind);
            gridRow.Select();
            return gridRow;
        }

        /// <summary>
        /// Add a row to the selection by index.
        /// </summary>
        public GridRow AddToSelection(int rowIndex)
        {
            var gridRow = this.GetRowByIndex(rowIndex);
            gridRow.AddToSelection();
            return gridRow;
        }

        /// <summary>
        /// Add a row to the selection by text in the given column.
        /// </summary>
        public GridRow AddToSelection(int columnIndex, string textToFind)
        {
            var gridRow = this.GetRowByValue(columnIndex, textToFind);
            gridRow.AddToSelection();
            return gridRow;
        }

        /// <summary>
        /// Remove a row from the selection by index.
        /// </summary>
        public GridRow RemoveFromSelection(int rowIndex)
        {
            var gridRow = this.GetRowByIndex(rowIndex);
            gridRow.RemoveFromSelection();
            return gridRow;
        }

        /// <summary>
        /// Remove a row from the selection by text in the given column.
        /// </summary>
        public GridRow RemoveFromSelection(int columnIndex, string textToFind)
        {
            var gridRow = this.GetRowByValue(columnIndex, textToFind);
            gridRow.RemoveFromSelection();
            return gridRow;
        }

        /// <summary>
        /// Get a row by index.
        /// </summary>
        public GridRow GetRowByIndex(int rowIndex)
        {
            this.PreCheckRow(rowIndex);
            var gridCell = this.GridPattern.GetItem(rowIndex, 0).AsGridCell();
            return gridCell.ContainingRow;
        }

        /// <summary>
        /// Get a row by text in the given column.
        /// </summary>
        public GridRow GetRowByValue(int columnIndex, string value)
        {
            return this.GetRowsByValue(columnIndex, value, 1).FirstOrDefault();
        }

        /// <summary>
        /// Get all rows where the value of the given column matches the given value.
        /// </summary>
        /// <param name="columnIndex">The column index to check.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="maxItems">Maximum numbers of items to return, 0 for all.</param>
        /// <returns>List of found rows.</returns>
        public GridRow[] GetRowsByValue(int columnIndex, string value, int maxItems = 0)
        {
            this.PreCheckColumn(columnIndex);
            var gridPattern = this.GridPattern;
            var returnList = new List<GridRow>();
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                var currentCell = gridPattern.GetItem(rowIndex, columnIndex).AsGridCell();
                if (currentCell.Value == value)
                {
                    returnList.Add(currentCell.ContainingRow);
                    if (maxItems > 0 && returnList.Count >= maxItems)
                    {
                        break;
                    }
                }
            }

            return returnList.ToArray();
        }

        private void PreCheckRow(int rowIndex)
        {
            if (this.RowCount <= rowIndex)
            {
                throw new Exception($"Grid contains only {this.RowCount} row(s) but index {rowIndex} was requested");
            }
        }

        private void PreCheckColumn(int columnIndex)
        {
            if (this.ColumnCount <= columnIndex)
            {
                throw new Exception($"Grid contains only {this.ColumnCount} columns(s) but index {columnIndex} was requested");
            }
        }
    }

    /// <summary>
    /// Header element for grids and tables.
    /// </summary>
    public class GridHeader : AutomationElement
    {
        public GridHeader(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public GridHeaderItem[] Columns
        {
            get
            {
                var headerItems = this.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem));
                return headerItems.Select(x => x.AsGridHeaderItem()).ToArray();
            }
        }
    }

    /// <summary>
    /// Header item for grids and tables.
    /// </summary>
    public class GridHeaderItem : AutomationElement
    {
        public GridHeaderItem(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public string Text => this.Properties.Name.Value;
    }

    /// <summary>
    /// Row element for grids and tables.
    /// </summary>
    public class GridRow : SelectionItemAutomationElement
    {
        public GridRow(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public GridCell[] Cells
        {
            get
            {
                var cells = this.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem).Not());
                return cells.Select(x => x.AsGridCell()).ToArray();
            }
        }

        public GridHeaderItem Header
        {
            get
            {
                var headerItem = this.FindFirstChild(this.ConditionFactory.ByControlType(ControlType.HeaderItem));
                return headerItem?.AsGridHeaderItem();
            }
        }

        protected IScrollItemPattern ScrollItemPattern => this.Patterns.ScrollItem.Pattern;

        /// <summary>
        /// Find a cell by a given text.
        /// </summary>
        public GridCell FindCellByText(string textToFind)
        {
            return this.Cells.FirstOrDefault(cell => cell.Value.Equals(textToFind));
        }

        public GridRow ScrollIntoView()
        {
            this.ScrollItemPattern?.ScrollIntoView();
            return this;
        }
    }

    /// <summary>
    /// Cell element for grids and tables.
    /// </summary>
    public class GridCell : AutomationElement
    {
        public GridCell(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        public Grid ContainingGrid => this.GridItemPattern.ContainingGrid.Value.AsGrid();

        public GridRow ContainingRow
        {
            get
            {
                // Get the parent of the cell (which should be the row)
                var rowElement = this.Automation.TreeWalkerFactory.GetControlViewWalker().GetParent(this);
                return rowElement?.AsGridRow();
            }
        }

        public string Value => this.Properties.Name.Value;

        protected IGridItemPattern GridItemPattern => this.Patterns.GridItem.Pattern;

        protected ITableItemPattern TableItemPattern => this.Patterns.TableItem.Pattern;
    }
}