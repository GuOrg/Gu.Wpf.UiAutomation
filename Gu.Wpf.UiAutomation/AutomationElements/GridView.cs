namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    /// <summary>
    /// Element for grids and tables.
    /// </summary>
    public class GridView : Control
    {
        public GridView(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Gets the total row count.
        /// </summary>
        public virtual int RowCount => this.GridPattern.Current.RowCount;

        /// <summary>
        /// Gets the total column count.
        /// </summary>
        public int ColumnCount => this.GridPattern.Current.ColumnCount;

        /// <summary>
        /// Gets all column header elements.
        /// </summary>
        public IReadOnlyList<ColumnHeader> ColumnHeaders
        {
            get
            {
                if (this.TryFindFirst(
                    TreeScope.Children,
                    Condition.Header,
                    FromAutomationElement,
                    Retry.Time,
                    out var header))
                {
                    return header.FindAllChildren(
                        Condition.HeaderItem,
                        x => new ColumnHeader(x));
                }

                if (this.AutomationElement.TryGetTablePattern(out var tablePattern))
                {
                    return tablePattern.Current
                                       .GetColumnHeaders()
                                       .Select(x => new ColumnHeader(x))
                                       .ToArray();
                }

                throw new InvalidOperationException("Could not find ColumnHeaders");
            }
        }

        /// <summary>
        /// Gets all row header elements.
        /// </summary>
        public IReadOnlyList<RowHeader> RowHeaders
        {
            get
            {
                var headers = new List<RowHeader>();
                foreach (var row in this.Rows)
                {
                    var header = row.Header;
                    if (header?.Bounds.IsEmpty == false)
                    {
                        headers.Add(header);
                    }
                }

                return headers;
            }
        }

        /// <summary>
        /// Gets whether the data should be read primarily by row or by column.
        /// </summary>
        public RowOrColumnMajor RowOrColumnMajor => this.TablePattern.Current.RowOrColumnMajor;

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
                var gridPattern = this.AutomationElement.GridPattern();
                for (var i = 0; i < rowCount; i++)
                {
                    rows[i] = new GridRow(gridPattern.GetItem(i, 0).Parent());
                }

                return rows;
            }
        }

        /// <summary>
        /// Gets all selected items.
        /// </summary>
        public IReadOnlyList<UiElement> SelectedItems => this.SelectionPattern
                                                             .Current
                                                             .GetSelection()
                                                             .Select(FromAutomationElement)
                                                             .ToArray();

        /// <summary>
        /// Gets the first selected item or null otherwise.
        /// </summary>
        public UiElement SelectedItem => this.SelectedItems?.FirstOrDefault();

        protected GridPattern GridPattern => this.AutomationElement.GridPattern();

        protected TablePattern TablePattern => this.AutomationElement.TablePattern();

        protected SelectionPattern SelectionPattern => this.AutomationElement.SelectionPattern();

        public GridCell this[int row, int col] => new GridCell(this.GridPattern.GetItem(row, col));

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public GridRow Select(int rowIndex)
        {
            var gridRow = this.GetRowByIndex(rowIndex);
            if (WindowsVersion.IsWindows7())
            {
                gridRow.Cells[0].Click();
            }
            else
            {
                gridRow.Select();
            }

            return gridRow;
        }

        public GridRow Row(int row) => new GridRow(this.GridPattern.GetItem(row, 0).Parent());

        public RowHeader RowHeader(int row) => this.Row(row).Header;

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public GridCell Select(int row, int column)
        {
            var cell = this[row, column];
            cell.Select();
            return cell;
        }

        /// <summary>
        /// Select the first row by text in the given column.
        /// </summary>
        public GridRow Select(int columnIndex, string textToFind)
        {
            var gridRow = this.GetRowByValue(columnIndex, textToFind);
            if (WindowsVersion.IsWindows7())
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
        /// Get a row by index.
        /// </summary>
        public GridRow GetRowByIndex(int rowIndex)
        {
            this.PreCheckRow(rowIndex);
            var gridCell = new GridCell(this.GridPattern.GetItem(rowIndex, 0));
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
                var currentCell = new GridCell(gridPattern.GetItem(rowIndex, columnIndex));
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