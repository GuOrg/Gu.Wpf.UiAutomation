namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class DataGrid : Selector
    {
        public DataGrid(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public bool IsReadOnly
        {
            get
            {
                if (this.AutomationElement.TryFindFirst(TreeScope.Children, Condition.DataGridRow, out var firstRow))
                {
                    foreach (AutomationElement cell in firstRow.FindAllChildren(Condition.DataGridCell))
                    {
                        if (cell.IsKeyboardFocusable() &&
                            cell.TryGetValuePattern(out var valuePattern) &&
                            !valuePattern.Current.IsReadOnly)
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the total row count excluding new item placeholder.
        /// </summary>
        public int RowCount
        {
            get
            {
                var rowCount = this.GridPattern.Current.RowCount;
                if (rowCount == 0)
                {
                    return 0;
                }

                var cell = this.GridPattern.GetItem(rowCount - 1, 0);
                if (DataGridCell.NewItemPlaceHolderRegex.IsMatch(cell.Name()) &&
                    cell.Parent().BoundingRectangle().IsEmpty)
                {
                    return rowCount - 1;
                }

                return rowCount;
            }
        }

        /// <summary>
        /// Gets the total column count.
        /// </summary>
        public int ColumnCount => this.GridPattern.Current.ColumnCount;

        public DataGridColumnHeadersPresenter ColumnHeadersPresenter => (DataGridColumnHeadersPresenter)this.FindFirstChild(Condition.DataGridColumnHeadersPresenter);

        /// <summary>
        /// Gets all column header elements.
        /// </summary>
        public IReadOnlyList<DataGridColumnHeader> ColumnHeaders
        {
            get
            {
                if (this.TryFindFirst(TreeScope.Children, Condition.DataGridColumnHeadersPresenter, FromAutomationElement, Retry.Time, out var header))
                {
                    return header.FindAllChildren(Condition.HeaderItem, x => (DataGridColumnHeader)FromAutomationElement(x));
                }

                if (this.AutomationElement.TryGetTablePattern(out var tablePattern))
                {
                    return tablePattern.Current
                                       .GetColumnHeaders()
                                       .Select(x => (DataGridColumnHeader)FromAutomationElement(x))
                                       .ToArray();
                }

                throw new InvalidOperationException("Could not find ColumnHeaders");
            }
        }

        /// <summary>
        /// Gets all row header elements.
        /// </summary>
        public IReadOnlyList<DataGridRowHeader> RowHeaders
        {
            get
            {
                var headers = new List<DataGridRowHeader>();
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
        /// Returns the rows which are currently visible to Interop.UIAutomationClient. Might not be the full list (eg. in virtualized lists)!
        /// Use <see cref="GetRowByIndex" /> to make sure to get the correct row.
        /// </summary>
        public virtual IReadOnlyList<DataGridRow> Rows
        {
            get
            {
                var rowCount = this.RowCount;
                var rows = new DataGridRow[rowCount];
                var gridPattern = this.AutomationElement.GridPattern();
                for (var i = 0; i < rowCount; i++)
                {
                    rows[i] = new DataGridRow(gridPattern.GetItem(i, 0).Parent());
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

        public GridPattern GridPattern => this.AutomationElement.GridPattern();

        public TablePattern TablePattern => this.AutomationElement.TablePattern();

        public ItemContainerPattern ItemContainerPattern => this.AutomationElement.ItemContainerPattern();

        public DataGridCell this[int row, int col] => (DataGridCell)FromAutomationElement(this.GridPattern.GetItem(row, col));

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public DataGridRow Select(int rowIndex)
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

        public DataGridRow Row(int row) => (DataGridRow)FromAutomationElement(this.GridPattern.GetItem(row, 0).Parent());

        public DataGridRowHeader RowHeader(int row) => this.Row(row).Header;

        /// <summary>
        /// Select a row by index.
        /// </summary>
        public DataGridCell Select(int row, int column)
        {
            var cell = this[row, column];
            cell.Select();
            return cell;
        }

        /// <summary>
        /// Select the first row by text in the given column.
        /// </summary>
        public DataGridRow Select(int columnIndex, string textToFind)
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
        public DataGridRow GetRowByIndex(int rowIndex)
        {
            this.PreCheckRow(rowIndex);
            return (DataGridRow)FromAutomationElement(this.GridPattern.GetItem(rowIndex, 0).Parent());
        }

        /// <summary>
        /// Get a row by text in the given column.
        /// </summary>
        public DataGridRow GetRowByValue(int columnIndex, string value)
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
        public IReadOnlyList<DataGridRow> GetRowsByValue(int columnIndex, string value, int maxItems = 0)
        {
            this.PreCheckColumn(columnIndex);
            var gridPattern = this.GridPattern;
            var returnList = new List<DataGridRow>();
            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                var currentCell = gridPattern.GetItem(rowIndex, columnIndex);
                if (currentCell.TryGetValuePattern(out var valuePattern) &&
                    valuePattern.Current.Value == value)
                {
                    returnList.Add((DataGridRow)FromAutomationElement(currentCell.Parent()));
                    if (maxItems > 0 && returnList.Count >= maxItems)
                    {
                        break;
                    }
                }
            }

            return returnList;
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
