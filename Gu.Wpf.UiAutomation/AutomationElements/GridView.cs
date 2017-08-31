namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Element for grids and tables.
    /// </summary>
    public abstract class GridView : Control
    {
        protected GridView(BasicAutomationElementBase basicAutomationElement)
            : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// Gets the total row count.
        /// </summary>
        public int RowCount
        {
            get
            {
                var gridPattern = this.GridPattern;
                var rowCount = gridPattern.RowCount;
                if (rowCount == 0)
                {
                    return 0;
                }

                var cell = new GridCell(gridPattern.GetItem(rowCount - 1, 0).BasicAutomationElement);
                if (cell.IsNewItemPlaceholder &&
                    cell.IsReadOnly)
                {
                    return rowCount - 1;
                }

                return rowCount.Value;
            }
        }

        /// <summary>
        /// Gets the total column count.
        /// </summary>
        public int ColumnCount => this.GridPattern.ColumnCount.Value;

        /// <summary>
        /// Gets all column header elements.
        /// </summary>
        public IReadOnlyList<ColumnHeader> ColumnHeaders
        {
            get
            {
                if (OperatingSystem.IsWindows7())
                {
                    // ReSharper disable once UnusedVariable for some reason this warmup is needed.
                    // Nasty but the only way I could get it to work.
                    var columnHeaderItems = this.Rows.FirstOrDefault()
                                                ?.Cells.FirstOrDefault()
                                                ?.Patterns
                                                .TableItem
                                                .PatternOrDefault
                                                ?.ColumnHeaderItems;
                }

                return this.TablePattern.ColumnHeaders.Value
                           .Select(x => new ColumnHeader(x.BasicAutomationElement))
                           .ToArray();
            }
        }

        /// <summary>
        /// Gets all row header elements.
        /// </summary>
        public IReadOnlyList<RowHeader> RowHeaders
        {
            get
            {
                var rowCount = this.RowCount;
                var rows = new RowHeader[rowCount];
                var gridPattern = this.Patterns.Grid.Pattern;
                for (var i = 0; i < rowCount; i++)
                {
                    var header = new GridRow(gridPattern.GetItem(i, 0).Parent.BasicAutomationElement).Header;
                    if (header == null)
                    {
                        return new RowHeader[0];
                    }

                    rows[i] = header;
                }

                return rows;
            }
        }

        /// <summary>
        /// Gets whether the data should be read primarily by row or by column.
        /// </summary>
        public RowOrColumnMajor RowOrColumnMajor => this.TablePattern.RowOrColumnMajor.Value;

        /// <summary>
        /// Returns the rows which are currently visible to Interop.UIAutomationClient. Might not be the full list (eg. in virtualized lists)!
        /// Use <see cref="GetRowByIndex" /> to make sure to get the correct row.
        /// </summary>
        public virtual IReadOnlyList<GridRow> Rows
        {
            get
            {
                var rowCount = this.RowCount;
                var rows = new GridRow[rowCount];
                var gridPattern = this.Patterns.Grid.Pattern;
                for (var i = 0; i < rowCount; i++)
                {
                    rows[i] = new GridRow(gridPattern.GetItem(i, 0).Parent.BasicAutomationElement);
                }

                return rows;
            }
        }

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public IReadOnlyList<GridRow> SelectedItems => this.SelectionPattern.Selection.Value.Select(x => new GridRow(x.BasicAutomationElement)).ToArray();

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
            if (OperatingSystem.IsWindows7())
            {
                gridRow.Cells[0].Click();
            }
            else
            {
                gridRow.Select();
            }

            return gridRow;
        }

        /// <summary>
        /// Select the first row by text in the given column.
        /// </summary>
        public GridRow Select(int columnIndex, string textToFind)
        {
            var gridRow = this.GetRowByValue(columnIndex, textToFind);
            if (OperatingSystem.IsWindows7())
            {
                gridRow.Cells[0].Click();
            }
            else
            {
                gridRow.Select();
            }

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
        public IReadOnlyList<GridRow> GetRowsByValue(int columnIndex, string value, int maxItems = 0)
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
                throw new Exception($"GridView contains only {this.RowCount} row(s) but index {rowIndex} was requested");
            }
        }

        private void PreCheckColumn(int columnIndex)
        {
            if (this.ColumnCount <= columnIndex)
            {
                throw new Exception($"GridView contains only {this.ColumnCount} columns(s) but index {columnIndex} was requested");
            }
        }
    }
}