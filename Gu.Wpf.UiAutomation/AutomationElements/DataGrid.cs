namespace Gu.Wpf.UiAutomation
{
    using System.Windows.Automation;

    public class DataGrid : GridView
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

        public override int RowCount
        {
            get
            {
                var rowCount = this.GridPattern.Current.RowCount;
                if (rowCount == 0)
                {
                    return 0;
                }

                var cell = new GridCell(this.GridPattern.GetItem(rowCount - 1, 0));
                if (cell.IsNewItemPlaceholder &&
                    cell.IsReadOnly)
                {
                    return rowCount - 1;
                }

                return rowCount;
            }
        }
    }
}
